using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness
    {
        IEnumerable<MemberDto> GetMembers();
        Task<Response<MemberDeleteDto>> Delete(int id);
        Response<MemberModel> PostMember(MemberCreateDto memberDto);

    }
}