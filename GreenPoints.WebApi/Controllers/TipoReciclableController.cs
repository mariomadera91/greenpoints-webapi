using GreenPoints.Services;
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
    }
}
