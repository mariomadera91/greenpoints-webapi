using GreenPoints.Domain;
using GreenPoints.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [Authorize]
        [HttpGet]
        public ActionResult<List<Premio>> Get()
        {
            var premios = _premioService.Get();

            return Ok(premios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var premio = _premioService.GetById(id);

            if(premio == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(premio);
            }
        }
    }
}
