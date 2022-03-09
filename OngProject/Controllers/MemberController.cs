using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly IMembers _members;

        public MemberController(IMembers member)
        {
            _members = member;

        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("Members")]
        public IActionResult GetMembers()
        {
            return Ok(_members.GetMembers());
        }
        [Authorize]
        [HttpDelete("Members/{id:int}")]

        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = (await _members.Delete(id));
            if (member.Errors != null)
            {
                return StatusCode(404, member);
            }
            return Ok(member);
        }
    }
}
