using GreenPoints.Domain;
using GreenPoints.Services;
using GreenPoints.Services.Interfaces;
using GreenPoints.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GreenPoints.WebApi.Controllers
{
    [Route("sponsor")]
    [ApiController]
    public class SponsorController : Controller
    {
        private readonly ISponsorService _sponsorService;

        public SponsorController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostSponsor([FromBody] CreateSponsorDto sponsorModel)
        {
            if(sponsorModel == null || sponsorModel.Image == null || string.IsNullOrEmpty(sponsorModel.Nombre))
            {
                return BadRequest("Datos incorrectos");
            }

            _sponsorService.AddSponsor(sponsorModel);

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<SponsorDto>> Get()
        {
            var sponsors = _sponsorService.Get();

            return Ok(sponsors);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var sponsor = _sponsorService.GetDetailById(id);

            if (sponsor == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sponsor);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update([FromBody] SponsorDto sponsorDto)
        {
            _sponsorService.Update(sponsorDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _sponsorService.Delete(id);
            return Ok();

        }

        [HttpGet]
        [Route("image")]
        public IActionResult GetSponsorImage(string name)
        {
            var imageDto = _sponsorService.GetImage(name);
            return File(imageDto.Image, imageDto.ContentType);
        }
    }
}
