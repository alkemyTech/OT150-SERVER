using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
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
        [HttpGet("Users")]
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
        [HttpDelete("Users/{id}")]
        public async Task<ActionResult> Delete(int id)

        {

            var response = await _userBusiness.DeleteUser(id);
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
        [HttpPut("Users/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UserUpdateDto userUpdateDto)
        {

            if (ModelState.IsValid)
            {
                var updateResult = await _userBusiness.UpdateUser(id, userUpdateDto);

                if (updateResult.Succeeded != true)
                {
                    return StatusCode(404, updateResult);
                }
                return Ok(updateResult);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}