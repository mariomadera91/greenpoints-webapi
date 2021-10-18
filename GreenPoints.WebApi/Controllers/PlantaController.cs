using GreenPoints.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GreenPoints.WebApi.Controllers
{
    [ApiController]
    [Route("planta")]
    public class PlantaController : Controller
    {
        private readonly IPlantaService _plantaService;

        public PlantaController(IPlantaService plantaService)
        {
            _plantaService = plantaService;
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<List<PlantaSearchDto>> Search()
        {
            var plantas = _plantaService.GetSearch();

            return Ok(plantas);
        }
    }
}
