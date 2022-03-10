using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;

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
    }
}
