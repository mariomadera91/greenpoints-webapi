using GreenPoints.Services;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("punto-reciclaje")]
    [ApiController]
    public class PuntoReciclajeController : Controller
    {
        private readonly IPuntoReciclajeService _puntoReciclajeService;
        private readonly ITipoReciclableService _tipoReciclableService;
        public PuntoReciclajeController(
            IPuntoReciclajeService puntoReciclajeService,
            ITipoReciclableService tipoReciclableService)
        {
            _puntoReciclajeService = puntoReciclajeService;
            _tipoReciclableService = tipoReciclableService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetPuntoReciclaje([FromQuery] int? tipoId)
        {
            var puntos = _puntoReciclajeService.Get(tipoId);
            return Ok(puntos);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostPuntoReciclaje([FromBody] PuntoReciclajeModel puntoModel)
        {
            _puntoReciclajeService.Create(new CreatePuntoReciclajeDto()
            {
                UserName = puntoModel.Email,
                CustomerName = puntoModel.CustomerName,
                Document = puntoModel.Document,
                Latitud = puntoModel.Latitud,
                Longitud = puntoModel.Longitud,
                Direccion = puntoModel.Direccion,
                Password = HashHelper.GetSHA256(puntoModel.Password),
                Materials = puntoModel.Materials
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("tipo-reciclables")]
        public ActionResult GetTipoReciclables([FromQuery] int puntoId, [FromQuery] bool onlyOpenedLote = true)
        {
            var tipoReciclables = _tipoReciclableService.GetByPunto(puntoId, onlyOpenedLote);

            return Ok(tipoReciclables);
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] PuntoUpdateDto puntoUpdateDto)
        {
            _puntoReciclajeService.Update(puntoUpdateDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetByPuntoId(int id)
        {

            var punto = _puntoReciclajeService.GetByPuntoId(id);
            return Ok(punto);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _puntoReciclajeService.Delete(id);
            return Ok();
        }
    }
}
