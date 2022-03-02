
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;


namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly ISlideBusiness _slideBusiness;

        public SlideController(ISlideBusiness slideBusiness)
        {
            _slideBusiness = slideBusiness;
        }


        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var character = _slideBusiness.showDetailSlide(id);
            if (character == null) return NotFound();
            return Ok(character);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet()]
        public IActionResult GetSlide()
        {
            return Ok(_slideBusiness.GetSlides());
        }

    }
}
