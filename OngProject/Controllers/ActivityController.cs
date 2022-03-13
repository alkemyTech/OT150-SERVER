using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    
    public class ActivityController : ControllerBase
    {
        private readonly IActivityBusiness _activityBusiness;

        public ActivityController(IActivityBusiness activityBusiness)
        {
            _activityBusiness = activityBusiness;
        }



        /// POST: Activities
        /// <summary>
        /// Create new activity
        /// </summary>
        /// <remarks>
        /// Create new activity
        /// </remarks>
        /// <param name="activityBusiness">Activity data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The activity was created.</response>        

        ///<returns></returns>
        [HttpPost("Activities")]
        [Authorize]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>),StatusCodes.Status200OK)]
     
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        public IActionResult Activities([FromForm] ActivityDto activityBusiness)
        {

            if (ModelState.IsValid)
            {

                return Ok(_activityBusiness.Create(activityBusiness));

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        /// POST: Activities
        /// <summary>
        /// Update activity
        /// </summary>
        /// <remarks>
        /// Ask activity id, if it exists,  the activity is updated with the new data. 
        /// </remarks>
        /// <param name="id">Activity Id to update.</param>
        /// <param name="activityUpdateDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update activities.</response>
        /// <response code="200">OK. The activity was updated.</response>        
        /// <response code="404">NotFound. The activity not found.</response>     
        ///<returns></returns>
        
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ActivityDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status403Forbidden)]
        [Authorize(Roles="Admin")]
        [HttpPut("activities/{id}")]
        public async Task<IActionResult>Update(int id,[FromForm]ActivityUpdateDto activityUpdateDto)
        {
            var updatedActivity = await _activityBusiness.Update(id, activityUpdateDto);
            if (ModelState.IsValid)
            {
                if (updatedActivity.Errors != null)
                {
                    return StatusCode(404, updatedActivity);
                }
                return Ok(updatedActivity);
            }
            else
            {
                return BadRequest(ModelState);
            }
          
        }
    }
}
