using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

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

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public IActionResult GetSlide()
        {
            return Ok(_slideBusiness.GetSlides());
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Post([FromForm] SlidePostDto slidePostDto)
        {

           
            if (ModelState.IsValid)
            {
                var response = await _slideBusiness.Post(slidePostDto);
                if (response.Errors != null)
                {
                    if (response.Errors[0].Equals("The organization not found"))
                    {
                        return StatusCode(404, response);
                    }
                    return StatusCode(400, response);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] SlidePutDto slideDto)
        {

            var updateResult = await _slideBusiness.Update(id, slideDto);
            if (ModelState.IsValid)
            {
                if (updateResult.Errors != null)
                {
                    return StatusCode(404, updateResult);
                }
                return Ok(updateResult);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            var slide = (await _slideBusiness.Delete(id));
            if (slide.Errors != null)
            {
                return StatusCode(404, slide);
            }
            return Ok(slide);
        }
    }
}
