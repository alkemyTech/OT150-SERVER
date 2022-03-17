using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        /// POST: Members
        /// <summary>
        /// Create new member
        /// </summary>
        /// <remarks>
        /// Create new member
        /// </remarks>
        /// <param name="membersBusiness">Member data transfer object.</param>
        /// <response code="401">Unauthorized.Invalid Token or it wasn't provided.</response>  
        /// <response code="500">Server Error.</response>  
        /// <response code="200">OK. The member was created.</response>        
        ///<returns></returns>
        [HttpPost("Members")]
        [Authorize]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<MemberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
        public IActionResult Members([FromForm] MemberDto memberDto)
        {

            if (ModelState.IsValid)
            {

                return Ok(_members.Create(memberDto));

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// GET: Members
        /// <summary>
        /// Get a member list.
        /// </summary>
        /// <remarks>
        /// Get a member list.
        /// </remarks>
        /// <response code="200">OK. These are the members.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("Members")]
        [ProducesResponseType(typeof(List<MemberDto>), StatusCodes.Status200OK)]
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

        /// PUT: Members
        /// <summary>
        /// Updates a member
        /// </summary>
        /// <remarks>
        /// Update a member, validate and store in the database
        /// </remarks>
        /// <param name="id">Member Id to update.</param>
        /// <param name="memberPutDto"></param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The member was updated.</response>        
        /// <response code="404">NotFound. The member was not found.</response>     
        ///<returns></returns>
        [HttpPut("Members/{id}")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<MemberPutDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<MemberPutDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int id, [FromForm] MemberPutDto memberPutDto)
        {

            var updateResult = await _members.Update(id, memberPutDto);
            if (ModelState.IsValid)
            {
                if (updateResult.Errors != null)
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

        /// DELETE: Members
        /// <summary>
        /// Deletes a memeber
        /// </summary>
        /// <remarks>
        /// Validates the existence of the member and deletes it 
        /// </remarks>
        /// <param name="id">Member Id to delete.</param>
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<response code="403">Unauthorized. Your role doesn't allow you to update testimonials.</response>
        /// <response code="200">OK. The member was deleted.</response>        
        /// <response code="404">NotFound. The member was not found.</response>     
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status500InternalServerError)]
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
