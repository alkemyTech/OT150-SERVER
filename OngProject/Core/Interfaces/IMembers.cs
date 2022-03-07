using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMembers
    {
        IEnumerable<MemberDto> GetMembers();
        Task<Response<MemberDeleteDto>> Delete(int id);
    }
}