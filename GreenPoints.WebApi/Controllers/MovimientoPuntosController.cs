using GreenPoints.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("movimiento-puntos")]
    [ApiController]
    public class MovimientoPuntosController : Controller
    {
        private readonly IMovimientoPuntosService _movimientoPuntosService;

        public MovimientoPuntosController(IMovimientoPuntosService movimientoPuntosService)
        {
            _movimientoPuntosService = movimientoPuntosService;
        }

        [HttpGet]
        public ActionResult Get(int socioId)
        {
            if (socioId == 0)
            {
                return BadRequest();
            }

            var movimientos = _movimientoPuntosService.GetBySocio(socioId);
            return Ok(movimientos);
        }
    }
}
