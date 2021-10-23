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
        
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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

            var usuario = _usuarioService.Get(loginModel.User, HashHelper.GetSHA256(loginModel.Password));

            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(usuario);
        }

    }        
}
