using GreenPoints.Domain.Entities;
using GreenPoints.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenPoints.WebApi.Controllers
{
    [Route("premio")]
    [ApiController]
    public class PremioController : Controller
    {
        private readonly IPremioService _premioService;

        public PremioController(IPremioService premioService)
        {
            _premioService = premioService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Premio>>> Get()
        {
            return await _premioService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Premio>> GetById(int id)
        {
            return await _premioService.GetById(id);
        }
        
    }
}
