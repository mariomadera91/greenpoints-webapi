using GreenPoints.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using GreenPoints.WebApi.Models;

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
        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostPlanta([FromBody] CreatePlantaDto plantaDto)
        {
            _plantaService.AddPlanta(plantaDto);

            return Ok();
        }
        [HttpGet]
        [Route("search")]
        public ActionResult<List<PlantaSearchDto>> Search()
        {
            var plantas = _plantaService.GetSearch();

            return Ok(plantas);
        }
        [HttpGet]
        public ActionResult<List<PlantaDto>> Get()
        {
            var plantas = _plantaService.Get();

            return Ok(plantas);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var planta = _plantaService.GetDetailById(id);

            if (planta == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(planta);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] PlantaDto plantaDto)
        {
            _plantaService.Update(plantaDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _plantaService.Delete(id);
            return Ok();

        }
    }
}
