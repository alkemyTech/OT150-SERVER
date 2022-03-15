using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
     
    [ApiController]
  
    public class TestimonialsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestimonialsBussines _testimonialsBussines;
        public TestimonialsController(IUnitOfWork unitOfWork, ITestimonialsBussines testimonialsBussines)
        {
            _unitOfWork = unitOfWork;
            _testimonialsBussines = testimonialsBussines;
        }

        /// POST: Testimonials
        /// <summary>
        /// Creates new testimonial
        /// </summary>
        /// <remarks>
        /// Creates new testimonial
        /// </remarks>
        /// <param name="testimonialsBussines">Testimonials data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The activity was created.</response>        
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        [HttpPost("Testimonials/Post")]
        public async Task<IActionResult> Post([FromForm]TestimonialsPostDto testimonialPostDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _testimonialsBussines.Post(testimonialPostDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// DELETE: Testimonials
        /// <summary>
        /// Deletes a testimonial
        /// </summary>
        /// <remarks>
        /// Validates the existence of the member and deletes it 
        /// </remarks>
        /// <param name="id">Testimonial Id to delete.</param>
        /// <param name="testimonialsPutDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The testimonial was deleted.</response>        
        /// <response code="404">NotFound. The testimonial was not found.</response>     
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var idUser = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _testimonialsBussines.Delete(id, rol, idUser));
        }

        /// PUT: Testimonials
        /// <summary>
        /// Updates a testimonial
        /// </summary>
        /// <remarks>
        /// Update testimonial, validate and store in the database
        /// </remarks>
        /// <param name="id">Testimonial Id to update.</param>
        /// <param name="testimonialsPutDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The testimonial was updated.</response>        
        /// <response code="404">NotFound. The testimonial was not found.</response>     
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPut("public")]
        public IActionResult Put(TestimonialsPutDto testimonialsPutDto)
        {
            return Ok(_testimonialsBussines.PutTestimonials(testimonialsPutDto));

        } 

    }
}
