using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenPoints.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        private IConfiguration _configuration { get; set; }

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
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
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UsuarioDto()
            {
                User = user.UserName,
                Imagen = null,
                Rol = user.Rol,
                Token = ((JwtSecurityToken)token).RawData
            };
        }
    }
}
