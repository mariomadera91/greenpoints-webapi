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
        public ActionResult GetByPunto(int puntoId)
        {
            var tipoReciclables = _tipoReciclableService.GetByPunto(puntoId);

            return Ok(tipoReciclables);
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
    }
}
