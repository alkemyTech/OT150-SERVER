using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class NewsController : ControllerBase
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly INewsBusiness _newsBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;

        public NewsController(ICommentBusiness commentBusiness, INewsBusiness newsBusiness, IUnitOfWork unitOfWork, IUriService uriService)
        {
            _commentBusiness = commentBusiness;
            _newsBusiness = newsBusiness;
            _unitOfWork = unitOfWork;
            _uriService = uriService;
        }

        /// GET: News
        /// <summary>
        /// Get a all the news
        /// </summary>
        /// <remarks>
        /// Get all the news
        /// </remarks>
        /// <response code="200">OK. These are all the categories.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetNews([FromQuery] PaginationParams paginationParams)
        {
            var news = _newsBusiness.GetAllNews(paginationParams);
            var metadata = new Metadata
            {
                TotalCount = news.TotalCount,
                PageSize = news.PageSize,
                CurrentPage = news.CurrentPage,
                TotalPages = news.TotalPages,
                HasNextPage = news.HasNextPage,
                HasPreviousPage = news.HasPreviousPage,
                NextPageUrl = _uriService.GetNextPage(paginationParams, "News").ToString(),
                PreviousPageUrl = _uriService.GetPreviousPage(paginationParams, "News").ToString()
            };
            var response = new PaginationResponse<IEnumerable<NewsDto>>(news)
            {
                Meta = metadata
            };
            return Ok(response);
        }

        /// GET: News
        /// <summary>
        /// Gets the comment filtered by news
        /// </summary>
        /// <remarks>
        /// Gets the comment filtered by news
        /// </remarks>
        /// <response code="200">OK. These are the comments.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}/comments")]
        public ActionResult<List<CommentDto>> GetCommentFilterByNews(int id)
        {
            var listaComments = _commentBusiness.showListCommentDto(id);
            return Ok(listaComments);
        }

        /// GET: Category/5
        /// <summary>
        /// Get the news by its ID
        /// </summary>
        /// <remarks>
        /// Get the news by its ID
        /// </remarks>
        /// <param name="id">Id of the news to get</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>
        /// <response code="403">Unauthorized. Your role doesn't allow you to get the category.</response>
        /// <response code="200">OK. This is the news to this id.</response>        
        /// <response code="404">NotFound. News not found.</response> 
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public ActionResult<NewsDto> GetNew(int id)
        {
            return Ok(_newsBusiness.GetNews(id));
        }


        /// POST: News
        /// <summary>
        /// Create news
        /// </summary>
        /// <remarks>
        /// Create news
        /// </remarks>
        /// <param name="newsBusiness">News data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The news were created.</response>        
        ///<returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        public IActionResult PostNews(NewsPostDto newsPost)
        {
            return Ok(_newsBusiness.NewsPost(newsPost));
        }

        /// PUT: News
        /// <summary>
        /// Updates news
        /// </summary>
        /// <remarks>
        /// Update news, validate and store in the database
        /// </remarks>
        /// <param name="id">News Id to update.</param>
        /// <param name="newsUpdateDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The news were updated.</response>        
        /// <response code="404">NotFound. The news were not found.</response>     
        ///<returns></returns>
        ///[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]NewsUpdateDto newsUpdate)
        {
            var updatedNews = await _newsBusiness.Update(id, newsUpdate);
            if (ModelState.IsValid)
            {
                if (updatedNews.Errors != null)
                {
                    return StatusCode(404, updatedNews);
                }
                return Ok(updatedNews);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// DELETE: News
        /// <summary>
        /// Deletes the news
        /// </summary>
        /// <remarks>
        /// Validates the existence of the news and deletes it 
        /// </remarks>
        /// <param name="id">News Id to delete.</param>
        /// <param name="newstDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The news was deleted.</response>        
        /// <response code="404">NotFound. The news were not found.</response>     
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> NewsDelete(int id)
        {
            var response = await _newsBusiness.DeleteNews(id);
            if (response.Succeeded == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
