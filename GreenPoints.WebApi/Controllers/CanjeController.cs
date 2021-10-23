using GreenPoints.Services;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [ApiController]
    [Route("canje")]
    public class CanjeController : Controller
    {
        private readonly ICanjeService _canjeService;

        public CanjeController(ICanjeService canjeService)
        {
            _canjeService = canjeService;
        }

        [HttpPost]
        public IActionResult Exchange([FromBody] CanjeModel exchangeModel)
        {
            var codigo = _canjeService.Post(exchangeModel.PremioId, exchangeModel.SocioId);
            return Ok(codigo);
        }
    }
}
