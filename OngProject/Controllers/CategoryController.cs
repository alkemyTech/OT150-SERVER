using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryBussines _categoryBusiness;
 
        private readonly IUriService _uriService;
        public CategoryController(ICategoryBussines categoryBussines, IUriService uriService)
        {
            _categoryBusiness = categoryBussines;
     
            _uriService = uriService;

        }

        [HttpGet("ListaCategorias")]
        [Authorize(Roles ="Admin")]
        public IActionResult ListaCategorias([FromQuery] PaginationParams paginationParams)
        {
            var category = _categoryBusiness.GetCategories(paginationParams);

            var metadata = new Metadata
            {
                TotalCount = category.TotalCount,
                PageSize = category.PageSize,
                CurrentPage = category.CurrentPage,
                TotalPages = category.TotalPages,
                HasNextPage = category.HasNextPage,
                HasPreviousPage = category.HasPreviousPage,
                NextPageUrl = _uriService.GetNextPage(paginationParams, "Categories").ToString(),
                PreviousPageUrl = _uriService.GetPreviousPage(paginationParams, "Categories").ToString()
            };

            var response = new PaginationResponse<IEnumerable<CategorieDto>>(category)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
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
