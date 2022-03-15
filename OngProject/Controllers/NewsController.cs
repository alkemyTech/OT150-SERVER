using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}/comments")]
        public ActionResult<List<CommentDto>> GetCommentFilterByNews(int id)
        {
            var listaComments = _commentBusiness.showListCommentDto(id);
            return Ok(listaComments);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public ActionResult<NewsDto> GetNew(int id)
        {
            return Ok(_newsBusiness.GetNews(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult PostNews(NewsPostDto newsPost)
        {
            return Ok(_newsBusiness.NewsPost(newsPost));
        }


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
