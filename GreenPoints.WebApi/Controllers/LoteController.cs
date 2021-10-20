using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GreenPoints.WebApi.Controllers
{
    [Route("lote")]
    public class LoteController : Controller
    {
        private readonly ILoteService _loteService;
        public LoteController(ILoteService loteService)
        {
            _loteService = loteService;
        }


        [HttpPost]
        public ActionResult Post([FromBody] LoteModel loteModel)
        {
            if (loteModel == null || loteModel.PuntoId == 0 || loteModel.TipoReciclableId == 0)
            {
                return BadRequest("Faltan argumentos");
            }

            var lote = _loteService.Post(loteModel.PuntoId, loteModel.TipoReciclableId);

            if(lote != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Ya éxiste un lote activo para el tipo de material seleccionado");
            }
            
        }

        [HttpGet]
        public ActionResult GetByPunto(int puntoId)
        {
            if (puntoId == 0)
            {
                return BadRequest();
            }

            var lotes = _loteService.Get(puntoId);

            return Ok(lotes);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var lote = _loteService.GetbyId(id);
            return Ok(lote);
        }

        [HttpPut]
        public ActionResult Put([FromBody] UpdateLoteDto updateLoteDto)
        {
            if (updateLoteDto == null || updateLoteDto.PlantaId == 0)
            {
                return BadRequest();
            }

            _loteService.Update(updateLoteDto.LoteId, updateLoteDto.PlantaId);

            return Ok();
        }
    }
}
