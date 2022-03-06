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
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    //[Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Upload(IFormFile image)
        {
            var response = await _imagesBusiness.UploadFileAsync(image);
            return Ok(response);
        }

        [HttpDelete("users/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bajaLogica = await _userBusiness.DeleteUser(id);
            if (bajaLogica == false) return NotFound();
            else return Ok();
        }
    }
}