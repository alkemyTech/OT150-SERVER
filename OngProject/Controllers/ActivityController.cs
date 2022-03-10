using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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

        [HttpPost("Activities")]
        [Authorize]
        public IActionResult Activities([FromBody] ActivityDto activityBusiness)
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
