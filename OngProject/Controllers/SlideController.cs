using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : Controller
    {
        private readonly ISlide _slide;
        public SlideController(ISlide slide)
        {
            _slide = slide;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("Slides")]
        public IActionResult GetSlides()
        {
            return Ok(_slide.GetSlides());
        }
    }
}
