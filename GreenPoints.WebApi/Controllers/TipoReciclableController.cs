using GreenPoints.Services;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("tipo-reciclable")]
    [ApiController]
    public class TipoReciclableController : Controller
    {
        private readonly ITipoReciclableService _tipoReciclableService;
        public TipoReciclableController(ITipoReciclableService tipoReciclableService)
        {
            _tipoReciclableService = tipoReciclableService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get()
        {
            var tipoReciclables = _tipoReciclableService.Get();

            return Ok(tipoReciclables);
        }

        [HttpGet("{puntoId}")]
        public ActionResult GetByPunto(int puntoId, bool onlyOpenedLote = true)
        {
            var tipoReciclables = _tipoReciclableService.GetByPunto(puntoId, onlyOpenedLote);

            return Ok(tipoReciclables);
        }

        [HttpGet]
        [Route("tipo/{id:int}")]
        public ActionResult GetById(int id)
        {
            var tipo = _tipoReciclableService.GetDetailById(id);

            if (tipo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tipo);
            }
        }

        [HttpGet]
        [Route("image")]
        public IActionResult GetTipoReciclableImage(string name)
        {
            var imageDto = _tipoReciclableService.GetImage(name);
            return File(imageDto.Image, imageDto.ContentType);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostTipoReciclable([FromBody] TipoReciclableModel tipoReciclableModel)
        {
            _tipoReciclableService.AddTipoReciclable(new CreateTipoReciclableDto()
            {
                Nombre = tipoReciclableModel.Nombre,
                Points = tipoReciclableModel.Points
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] TipoReciclableDto tipoDto)
        {
            _tipoReciclableService.Update(tipoDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _tipoReciclableService.Delete(id);
            return Ok();

        }
    }
}
