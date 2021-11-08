using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public ActionResult Login([FromBody] LoginModel loginModel)
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

        [AllowAnonymous]
        [HttpPost]
        [Route("reset")]
        public async Task<ActionResult> Reset([FromBody] ResetModel model)
        {
            if(model == null || string.IsNullOrEmpty(model.Email))
            {
                return BadRequest();
            }

            await _usuarioService.Reset(model.Email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] UsuarioPassUpdateDto usuarioPassDto)
        {
            var response = _usuarioService.Update(usuarioPassDto);

            if(!string.IsNullOrEmpty(response))
            {
                return BadRequest(response);
            }

            return Ok();
        }

    }        
}
