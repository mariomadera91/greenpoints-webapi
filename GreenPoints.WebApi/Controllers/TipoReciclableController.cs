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

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var tipoReciclable = _tipoReciclableService.GetById(id);

            return Ok(tipoReciclable);
        }

        //[HttpGet]
        //[Route("tipo/{id:int}")]
        //public ActionResult GetById(int id)
        //{
        //    var tipo = _tipoReciclableService.GetDetailById(id);

        //    if (tipo == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(tipo);
        //    }
        //}

        [HttpGet]
        [Route("image")]
        public IActionResult GetTipoReciclableImage(string name)
        {
            var imageDto = _tipoReciclableService.GetImage(name);
            return File(imageDto.Image, imageDto.ContentType);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostTipoReciclable([FromBody] CreateTipoReciclableDto tipoReciclableModel)
        {
            _tipoReciclableService.AddTipoReciclable(tipoReciclableModel);

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
