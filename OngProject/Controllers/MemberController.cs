using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly IMembers _members;

        public MemberController(IMembers member)
        {
            _members = member;

        }
        
        //[Authorize(Roles = "Administrador")]
        [HttpGet("Members")]
        public IActionResult GetMembers()
        {
            return Ok(_members.GetMembers());
        }

    }
}
