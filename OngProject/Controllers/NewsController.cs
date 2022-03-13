using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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

        public NewsController(ICommentBusiness commentBusiness, INewsBusiness newsBusiness, IUnitOfWork unitOfWork)
        {
            _commentBusiness = commentBusiness;
            _newsBusiness = newsBusiness;
            _unitOfWork = unitOfWork;
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
