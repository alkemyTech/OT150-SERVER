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


        /// GET: Users
        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <remarks>
        /// Get a member list.
        /// </remarks>
        /// <response code="200">OK. These are the users.</response>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
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

        /// POST: Users
        /// <summary>
        /// Create new user
        /// </summary>
        /// <remarks>
        /// Create new user
        /// </remarks>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The user was created.</response>        
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            var response = await _imagesBusiness.UploadFileAsync(image);
            return Ok(response);
        }

        /// DELETE: Users
        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <remarks>
        /// Validates that the id of the user making the request is equal to the id to delete, and deletes it 
        /// </remarks>
        /// <param name="id">User Id to delete.</param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The user was deleted.</response>        
        /// <response code="404">NotFound. The user was not found.</response>     
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
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

        /// PUT: Users
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <remarks>
        /// Validate, update and store in the database
        /// </remarks>
        /// <param name="id">User Id to update.</param>
        /// <param name="memberPutDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The user was updated.</response>        
        /// <response code="404">NotFound. The user was not found.</response>     
        ///<returns></returns>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<MemberPutDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<MemberPutDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
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