using GreenPoints.Services;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GreenPoints.WebApi.Controllers
{
    [ApiController]
    [Route("premio")]
    public class PremioController : Controller
    {
        private readonly IPremioService _premioService;

        public PremioController(IPremioService premioService)
        {
            _premioService = premioService;
        }

        [HttpGet]
        public ActionResult<List<PremioListDto>> Get()
        {
            var premios = _premioService.Get();

            return Ok(premios);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var premio = _premioService.GetDetailById(id);

            if(premio == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(premio);
            }
        }

        [HttpGet]
        [Route("image")]
        public IActionResult GetProductImage(string name)
        {
            var imageDto = _premioService.GetImage(name);
            return File(imageDto.Image, imageDto.ContentType);
        }

        [HttpPost]
        [Route("exchange")]
        public IActionResult Exchange([FromBody] ExchangeModel exchangeModel)
        {
            var codigo = _premioService.Exchange(exchangeModel.PremioId, exchangeModel.SocioId);
            return Ok(codigo);
        }
    }
}
