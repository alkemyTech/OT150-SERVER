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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CategoryPost(CategoryPostDto categoryPostDto)
        {
            return Ok(_categoryBusiness.PostCategory(categoryPostDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            var response = await _categoryBusiness.DeleteCategory(id);
            if (response.Succeeded == true)
            {
                return Ok(response);
            }
            return StatusCode(404, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            var updatedCategory = await _categoryBusiness.UpdateCategory(id, categoryUpdateDto);
            if (ModelState.IsValid)
            {
                if (updatedCategory.Succeeded == true)
                {
                    return Ok(updatedCategory);
                }
                return StatusCode(404, updatedCategory);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
