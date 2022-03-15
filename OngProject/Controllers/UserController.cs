using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ImagesBusiness _imagesBusiness;
        private readonly IConfiguration _configuration;

        public UserController(IUserBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _imagesBusiness = new ImagesBusiness(configuration);
            _configuration = configuration;
        }

        [HttpGet("Lista")]
        [Authorize]

        public async Task<IActionResult> Lista()
        {
            try
            {
                return Ok(_userBusiness.GetUsuarios());
            }
            catch
            {
                return BadRequest();
            }
        }      

        [HttpPost]
        [Route("image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            var response = await _imagesBusiness.UploadFileAsync(image);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("users")]
        public async Task<ActionResult> Delete()

        {
            var idUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
          
            var response = await _userBusiness.DeleteUser(idUser);
            if (response.Errors != null)
            {
                return StatusCode(404, response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UserUpdateDto userUpdateDto)
        {
            var idUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (ModelState.IsValid)
            {
                if (idUser == id || role.Value.Equals("Admin"))
                {
                    var updateResult = await _userBusiness.UpdateUser(id, userUpdateDto);

                    if (updateResult.Succeeded != true)
                    {
                        return StatusCode(404, updateResult);
                    }
                    return Ok(updateResult);
                }
                return StatusCode(403, new Response<object> { Succeeded = false, Message = "You can't edit this User" });
                }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}