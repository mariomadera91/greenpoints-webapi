using GreenPoints.Services;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("socio-reciclador")]
    [ApiController]
    public class SocioRecicladorController : Controller
    {
        private readonly ISocioRecicladorService _socioRecicladorService;
        private readonly IPremioService _premioService;

        public SocioRecicladorController(
            ISocioRecicladorService socioRecicladorService,
            IPremioService premioService)
        {
            _socioRecicladorService = socioRecicladorService;
            _premioService = premioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostSocioReciclador([FromBody] SocioRecicladorModel socioModel)
        {
            _socioRecicladorService.Create(new CreateSocioRecicladorDto()
            {
                BirthDate = socioModel.BirthDate,
                Email = socioModel.Email,
                FirstName = socioModel.FirstName,
                LastName = socioModel.LastName,
                Password = HashHelper.GetSHA256(socioModel.Password),
                ReferidoId = socioModel.ReferidoId
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSocioReciclador()
        {
            var sociosRecicladores = _socioRecicladorService.Get();

            return Ok(sociosRecicladores);
        }

        

        [AllowAnonymous]
        [HttpGet]
        [Route("puntos")]
        public ActionResult GetSocioRecicladorPuntos([FromQuery] int socioId)
        {
            var puntos = _socioRecicladorService.GetPuntos(socioId);

            return Ok(puntos);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("premios")]
        public ActionResult GetSocioRecicladorPremios([FromQuery] int socioId)
        {

            var premios = _premioService.GetSocioPremioBySocio(socioId);
            return Ok(premios);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("premio")]
        public ActionResult GetSocioRecicladorPremiosDetail([FromQuery] int socioPremioId)
        {

            var premio = _premioService.GetSocioPremio(socioPremioId);
            return Ok(premio);
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] SocioUpdateDto socioUpdateDto)
        {
            _socioRecicladorService.Update(socioUpdateDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Id")]
        public ActionResult GetBySocioId([FromQuery] int socioRecicladorId)
        {

            var socio = _socioRecicladorService.GetBySocioId(socioRecicladorId);
            return Ok(socio);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _socioRecicladorService.Delete(id);
            return Ok();
        }


        //[AllowAnonymous]
        //[HttpPost]
        //[Route("Referido")]
        //public ActionResult PostReferdio(string referidoMail)
        //{
        //    _socioRecicladorService.Referido(referidoMail);
        //    return Ok();
        //}
    }
}
