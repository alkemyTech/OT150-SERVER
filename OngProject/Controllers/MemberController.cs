using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;

namespace OngProject.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly IMembers _members;

        public MemberController(IMembers member)
        {
            _members = member;

        }
        
        [Authorize(Roles = "Administrador")]
        [HttpGet("Members")]
        public IActionResult GetMembers()
        {
            return Ok(_members.GetMembers());
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost("Members")]
        public IActionResult PostMember(MemberCreateDto memberDto)
        {
            var memberToCreate = _members.AddMember(memberDto);
            return Ok(memberToCreate);
        }
    }
}
