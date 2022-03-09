using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles="Admin")]
        [HttpPost("Testimonials")]
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


        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var idUser = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _testimonialsBussines.Delete(id, rol, idUser));
        }


    }
}
