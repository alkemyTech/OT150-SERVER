using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMembers
    {
        Response<MemberDto> Create(MemberDto memberDto);
        PagedList<MemberDto> GetMembers(PaginationParams paginationParams);
        Task<Response<MemberDto>> Update(int id, MemberPutDto memberPutDto);
        Task<Response<MemberDeleteDto>> Delete(int id);
    }
}