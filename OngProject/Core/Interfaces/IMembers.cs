using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface IMembers
    {
        IEnumerable<MemberDto> GetMembers();
        MemberModel AddMember(MemberCreateDto member);

    }
}