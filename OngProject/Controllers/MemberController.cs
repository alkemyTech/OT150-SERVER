using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly IMemberBusiness _members;

        public MemberController(IMemberBusiness member)
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
            return Ok(await _members.Delete(id));
        }
        [HttpPost("Members")]
        public IActionResult PostMember(MemberCreateDto memberDto)
        {
            return Ok(_members.PostMember(memberDto));
        }
    }
}
