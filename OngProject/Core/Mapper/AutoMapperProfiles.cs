using AutoMapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Post
            CreateMap<MemberCreateDto, MemberModel>();
        }
    }
}
