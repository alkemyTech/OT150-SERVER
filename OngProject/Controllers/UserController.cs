using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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
    
    [Authorize(Roles = "Admin")]
    [HttpGet("Lista")]
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
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [Route("image")]
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
    }
}