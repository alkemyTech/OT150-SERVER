using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentBusiness _commentBusiness;

        public CommentsController(ICommentBusiness commentBusiness)
        {
            _commentBusiness = commentBusiness;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            try
            {

                return Ok(_commentBusiness.GetComments());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
