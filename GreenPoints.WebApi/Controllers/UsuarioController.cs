using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISocioRecicladorService _socioRecicladorService;
        public UsuarioController(IUsuarioService usuarioService, 
                                 ISocioRecicladorService socioRecicladorService)
        {
            _usuarioService = usuarioService;
            _socioRecicladorService = socioRecicladorService;

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
            
            var usuario = _usuarioService.Get(loginModel.User, loginModel.Password);

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
                Password = socioModel.Password
            });

            return Ok();
        }
    }
}
