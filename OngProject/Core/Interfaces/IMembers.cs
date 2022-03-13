using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMembers
    {
        PagedList<MemberDto> GetMembers(PaginationParams paginationParams);
        Task<Response<MemberDeleteDto>> Delete(int id);
    }
}