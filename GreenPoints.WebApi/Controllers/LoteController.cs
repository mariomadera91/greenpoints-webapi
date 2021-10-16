using GreenPoints.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public ActionResult Get(int puntoId)
        {
            if (puntoId == 0)
            {
                return BadRequest();
            }

            var lotes = _loteService.Get(puntoId);

            return Ok(lotes);
        }
    }
}
