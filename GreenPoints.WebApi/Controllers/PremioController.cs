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
        public ActionResult GetById(int id, [FromQuery] bool admin = false)
        {
            var premio = _premioService.GetDetailById(id, admin);

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


        [HttpGet]
        [Route("top")]
        public IActionResult GetTop()
        {
            var premios = _premioService.GetTop();
            return Ok(premios);
        }
    
        [HttpPost]
        public IActionResult Post([FromBody] CreatePremioDto model)
        {
            _premioService.Post(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _premioService.Delete(id);
            return Ok();
        }
    }
}
