using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryBussines _categoryBusiness;

        public CategoryController(ICategoryBussines categoryBussines)
        {
            _categoryBusiness = categoryBussines;
        }

        [HttpGet("ListaCategorias")]
        public IActionResult ListaCategorias()
        {

            try
            {

                return Ok(_categoryBusiness.GetCategories());

            }

            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                return Ok(_categoryBusiness.GetCategory(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize/*(Roles = "Admin")*/]
        [HttpPost]
        public IActionResult CategoryPost(CategoryPostDto categoryPostDto)
        {
            return Ok(_categoryBusiness.PostCategory(categoryPostDto));
        }

    }
}
