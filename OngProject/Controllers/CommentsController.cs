using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Linq;
using System.Security.Claims;
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

        [Authorize(Roles = "Administrador")]
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

        [Authorize]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var rol = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var idUser = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _commentBusiness.DeleteComment(id, rol, idUser));
        }

    }
}
