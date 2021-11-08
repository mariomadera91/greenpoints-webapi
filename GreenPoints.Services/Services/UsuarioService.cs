using GreenPoints.Data;
using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
                return null;
            }

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

            var nombre = string.Empty;

            if (user.Rol == UserRol.SocioReciclador) 
            {
                var socioReciclador = _socioRecicladorRepository.GetByUsuarioId(user.Id);
                id = socioReciclador.Id;
                nombre = socioReciclador.Nombre;
            }
            else if (user.Rol == UserRol.PuntoReciclaje)
            {
                var puntoReciclaje = _puntoReciclajeRepository.GetPuntoReciclaje(user.Id);
                id = puntoReciclaje.Id;
                nombre = puntoReciclaje.Nombre;
            }

                return new UsuarioDto()
            {
                User = user.UserName,
                Nombre = nombre,
                Id = id,
                Imagen = null,
                Rol = user.Rol,
                Token = jwt_token
            };
        }

        public async Task Reset(string email)
        {
            var usuario = _usuarioRepository.GetByEmail(email);

            if(usuario != null)
            {
                var client = new MailjetClient(_configuration.GetSection("Mail:apiKey").Value, _configuration.GetSection("Mail:apiSecret").Value);

                var newPassword = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0,12);

                var emailToSend = new TransactionalEmailBuilder()
                       .WithFrom(new SendContact(_configuration.GetSection("Mail:from").Value))
                       .WithSubject("Green Points - Nueva Contraseña")
                       .WithHtmlPart($"<h2>Restablecimiento de contraseña</h2><p>Tu nueva contraseña es: { newPassword }</p><p>No respondas este mail automático</p>.")
                       .WithTo(new SendContact(email))
                       .Build();

                using (var scope = new TransactionScope())
                {
                    usuario.Password = GetSHA256(newPassword);
                    usuario.LastPasswordReset = DateTime.Now;

                    _usuarioRepository.Update(usuario);

                    // invoke API to send email
                    var response = client.SendTransactionalEmailAsync(emailToSend).GetAwaiter().GetResult();

                    scope.Complete();
                }
                    
            }
        }

        private string GetSHA256(string str)
        {
            var sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }

        public string Update(UsuarioPassUpdateDto usuarioPassDto)
        {
            var usuario = _usuarioRepository.GetById(usuarioPassDto.UsuarioId);

            if (usuario.Password == GetSHA256(usuarioPassDto.PasswordOld))
            {
                    var usu = new Usuario()
                    {
                        Id = usuarioPassDto.UsuarioId,
                        UserName = usuario.UserName,
                        Password = GetSHA256(usuarioPassDto.Password),
                        Rol = usuario.Rol,
                        Activo = usuario.Activo,
                        LastPasswordReset = DateTime.Now
                    };
                    _usuarioRepository.Update(usu);

                return string.Empty;
            }
            else
            {
                return "Passord Anterior no coincide";
            }
        }
    }
}
