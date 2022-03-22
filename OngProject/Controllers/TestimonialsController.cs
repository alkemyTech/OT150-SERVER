using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
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
        private readonly IUriService _uriService;
        public TestimonialsController(IUnitOfWork unitOfWork, ITestimonialsBussines testimonialsBussines, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _testimonialsBussines = testimonialsBussines;
            _uriService = uriService;
        }

        /// GET: Testimonials
        /// <summary>
        /// Get all testimonials
        /// </summary>
        /// <remarks>
        /// Get all testimonials
        /// </remarks>
        /// <param name="testimonialsBusiness">Testimonials data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The activity was created.</response>        
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(PagedList<TestimonialsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("Testimonials")]
        public IActionResult GetTestimonials([FromQuery] PaginationParams paginationParams)
        {
            var testimonials = _testimonialsBussines.GetAllTestimonials(paginationParams);
            var metadata = new Metadata
            {
                TotalCount = testimonials.TotalCount,
                PageSize = testimonials.PageSize,
                CurrentPage = testimonials.CurrentPage,
                TotalPages = testimonials.TotalPages,
                HasNextPage = testimonials.HasNextPage,
                HasPreviousPage = testimonials.HasPreviousPage,
                NextPageUrl = _uriService.GetNextPage(paginationParams, "Testimonials").ToString(),
                PreviousPageUrl = _uriService.GetPreviousPage(paginationParams, "Testimonials").ToString()
            };
            var response = new PaginationResponse<IEnumerable<TestimonialsDto>>(testimonials)
            {
                Meta = metadata
            };
            return Ok(response);
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
        /// <response code="200">OK. The Testimonial was created.</response>        
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<TestimonialsPostDto>), StatusCodes.Status200OK)]
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
            var deletedTestimonials = await _testimonialsBussines.Delete(id);
            if (ModelState.IsValid)
            {
                if (deletedTestimonials.Errors != null)
                {
                    return StatusCode(404, deletedTestimonials);
                }
                return Ok(deletedTestimonials);
            }
            else
            {
                return BadRequest(ModelState);
            }
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
        /// <response code="404">The testimonial was not found.</response>     
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<TestimonialsPutDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<TestimonialsPutDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPut("Testimonials/{id:int}")]
        public async Task<IActionResult> Put(int id,[FromForm]TestimonialsPutDto testimonialsPutDto)
        {
            var updatedTestimonials = await _testimonialsBussines.PutTestimonials(id, testimonialsPutDto);
            if (ModelState.IsValid)
            {
                if (updatedTestimonials.Errors != null)
                {
                    return StatusCode(404, updatedTestimonials);
                }
                return Ok(updatedTestimonials);
            }
            else
            {
                return BadRequest(ModelState);
            }

        } 

    }
}
