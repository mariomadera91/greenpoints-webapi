using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
namespace GreenPoints.WebApi.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISocioRecicladorService _socioRecicladorService;
        private readonly IPuntoReciclajeService _puntoReciclajeService;
        public UsuarioController(IUsuarioService usuarioService,
                                 ISocioRecicladorService socioRecicladorService,
                                 IPuntoReciclajeService puntoReciclajeService)
        {
            _usuarioService = usuarioService;
            _socioRecicladorService = socioRecicladorService;
            _puntoReciclajeService = puntoReciclajeService;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Post([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.User) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest();
            }

            var usuario = _usuarioService.Get(loginModel.User, GetSHA256(loginModel.Password));

            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("socio-reciclador")]
        public ActionResult PostSocioReciclador([FromBody] SocioRecicladorModel socioModel)
        {
            _socioRecicladorService.Create(new CreateSocioRecicladorDto()
            {
                BirthDate = socioModel.BirthDate,
                Email = socioModel.Email,
                FirstName = socioModel.FirstName,
                LastName = socioModel.LastName,
                Password = GetSHA256(socioModel.Password)
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("punto-reciclaje")]
        public ActionResult PostPuntoReciclaje([FromBody] PuntoReciclajeModel puntoModel)
        {
            _puntoReciclajeService.Create(new CreatePuntoReciclajeDto()
            {
                UserName = puntoModel.Username,
                CustomerName = puntoModel.CustomerName,
                Document = puntoModel.Document,
                Latitud = puntoModel.Latitud,
                Longitud = puntoModel.Longitud,
                Direccion = puntoModel.Direccion,
                Password = GetSHA256(puntoModel.Password),
                Materials = puntoModel.Materials
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("socio-reciclador")]
        public ActionResult GetSocioReciclador()
        {
            var sociosRecicladores = _socioRecicladorService.Get();

            return Ok(sociosRecicladores);
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }
    }
}
