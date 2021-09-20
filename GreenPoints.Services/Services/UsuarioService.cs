using GreenPoints.Data;
using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenPoints.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        private IConfiguration _configuration { get; set; }
        private ISocioRecicladorRepository _socioRecicladorRepository { get; set; }
        private IPuntoReciclajeRepository _puntoReciclajeRepository { get; set; }
        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration, ISocioRecicladorRepository socioReciclador, IPuntoReciclajeRepository puntoReciclajeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _socioRecicladorRepository = socioReciclador;
            _puntoReciclajeRepository = puntoReciclajeRepository;
        }

        public UsuarioDto Get(string userName, string password)
        {
            var user = _usuarioRepository.GetUsuario(userName, password);

            if(user == null)
            {
                throw new Exception("Datos incorrectos");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = _configuration.GetSection("jwt_key").Value;
            var issuer = "http://mysite.com";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("user", userName));

            var token = new JwtSecurityToken(issuer,  
                    issuer,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

             int? id = null;

            if (user.Rol == UserRol.SocioReciclador) 
            {
                var socioReciclador = _socioRecicladorRepository.GetSocioReciclador(user.Id);
                id = socioReciclador.Id;
            }
            else if (user.Rol == UserRol.PuntoReciclaje)
            {
                var puntoReciclaje = _puntoReciclajeRepository.GetPuntoReciclaje(user.Id);
                id = puntoReciclaje.Id;
            }

                return new UsuarioDto()
            {
                User = user.UserName,
                Id = id,
                Imagen = null,
                Rol = user.Rol,
                Token = jwt_token
            };
        }
    }
}
