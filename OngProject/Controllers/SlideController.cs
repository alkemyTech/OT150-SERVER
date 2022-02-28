using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("/Slides/{id:int}")]
        public ActionResult GetById(int id)
        {
            var slide = _slide.GetById(id);
            if (slide == null) return NotFound();
            return Ok(slide);
        }
    }
}
