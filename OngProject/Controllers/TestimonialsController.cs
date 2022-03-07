using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class TestimonialsController : Controller
    {



        private readonly ITestimonialsBussines _testimonialsBussines;
        private readonly IConfiguration _configuration;

        public TestimonialsController(ITestimonialsBussines testimonialsBussines, IConfiguration configuration)
        {

            _testimonialsBussines = testimonialsBussines;

            _configuration = configuration;

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {

                return Ok(_testimonialsBussines.Delete(id));

            }

            catch
            {
                return BadRequest();
            }
        }

    }
}
