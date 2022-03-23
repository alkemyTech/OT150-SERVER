using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
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

        /// GET: Category
        /// <summary>
        /// Get a category List.
        /// </summary>
        /// <remarks>
        /// Get a category List
        /// </remarks>
        /// <response code="200">OK. These are the categories.</response>
        [HttpGet("ListaCategorias")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CategorieDto>), StatusCodes.Status200OK)]
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

        /// GET: Category/5
        /// <summary>
        /// Get a category by its ID
        /// </summary>
        /// <remarks>
        /// Get a category by its ID
        /// </remarks>
        /// <param name="id">Id of the category to get</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>
        /// <response code="403">Unauthorized. Your role doesn't allow you to get the category.</response>
        /// <response code="200">OK. The Category was created.</response>        
        /// <response code="404">NotFound. The category not found.</response> 
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CategoryGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
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

        /// POST: Category
        /// <summary>
        /// Create new category
        /// </summary>
        /// <remarks>
        /// Create new category
        /// </remarks>
        /// <param name="CategoryPostDto">Category data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>
        /// <response code="403">Unauthorized. Your role doesn't allow you to update categories.</response>
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The Category was created.</response>
        ///<returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<CategorieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        public IActionResult CategoryPost(CategoryPostDto categoryPostDto)
        {
            return Ok(_categoryBusiness.PostCategory(categoryPostDto));
        }

        /// DELETE: Category/5
        /// <summary>
        /// Delete category
        /// </summary>
        /// <remarks>
        /// Ask category id, if it exists,  the category is deleted. 
        /// </remarks>
        /// <param name="id">Category Id to delete.</param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        /// <response code="403">Unauthorized. Your role doesn't allow you to delete categories.</response>
        /// <response code="200">OK. The category was deleted.</response>        
        /// <response code="404">NotFound. The category was not deleted.</response>     
        /// <response code="500">Server Error.</response>
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<CategorieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CategorieModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
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

        /// PUT: Category/5
        /// <summary>
        /// Update category
        /// </summary>
        /// <remarks>
        /// Ask category id, if it exists,  the category is updated with the new data. 
        /// </remarks>
        /// <param name="id">Category Id to update.</param>
        /// <param name="categoryUpdateDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        /// <response code="403">Unauthorized. Your role doesn't allow you to update categories.</response>
        /// <response code="200">OK. The category was updated.</response>        
        /// <response code="404">NotFound. The category not found.</response>     
        /// <response code="500">Server Error.</response>
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<CategorieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CategorieModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
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
