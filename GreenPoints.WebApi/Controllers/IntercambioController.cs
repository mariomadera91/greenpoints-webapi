using GreenPoints.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{

    [Route("intercambio")]
    [ApiController]
    public class IntercambioController : Controller
    {

        private readonly IIntercambioService _intercambioService;

        public IntercambioController(IIntercambioService intercambioService)
        {
            _intercambioService = intercambioService;
        }

        [HttpGet]
        public ActionResult Get(int socioId)
        {
            if(socioId == 0)
            {
                return BadRequest();
            }

            var intercambios = _intercambioService.GetBySocio(socioId);
            return Ok(intercambios);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var intercambio = _intercambioService.GetById(id);

            return Ok(intercambio);
        }
    }
}
