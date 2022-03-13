using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly IMembers _members;
        private readonly IUriService _uriService;

        public MemberController(IMembers member, IUriService uriService)
        {
            _members = member;
            _uriService = uriService;

        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("Members")]
        public IActionResult GetMembers([FromQuery] PaginationParams paginationParams)
        {
            var member = _members.GetMembers(paginationParams);

            var metadata = new Metadata
            {
                TotalCount = member.TotalCount,
                PageSize = member.PageSize,
                CurrentPage = member.CurrentPage,
                TotalPages = member.TotalPages,
                HasNextPage = member.HasNextPage,
                HasPreviousPage = member.HasPreviousPage,
                NextPageUrl = _uriService.GetNextPage(paginationParams, "Members").ToString(),
                PreviousPageUrl = _uriService.GetPreviousPage(paginationParams, "Members").ToString()
            };

            var response = new PaginationResponse<IEnumerable<MemberDto>>(member)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
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
