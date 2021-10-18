using GreenPoints.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPoints.WebApi.Controllers
{
    [Route("donacion")]
    [ApiController]
    public class DonacionController : Controller
    {
        private readonly IDonacionService _donacionService;

        public DonacionController(IDonacionService donacionService)
        {
            _donacionService = donacionService;
        }

        //CreateDonacionDto
        [HttpPost]
        public ActionResult Post([FromBody] CreateDonacionDto createDonacionDto)
        {
            if (createDonacionDto == null)
            {
                return BadRequest();
            }

            _donacionService.Post(createDonacionDto);

            return Ok();
        }
    }
}
