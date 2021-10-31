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

        public PuntoReciclajeController(IPuntoReciclajeService puntoReciclajeService)
        {
            _puntoReciclajeService = puntoReciclajeService;
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
    }
}
