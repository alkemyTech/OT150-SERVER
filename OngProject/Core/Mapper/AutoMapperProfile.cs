using AutoMapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<MemberCreateDto, MemberModel>();

        }
    }
}
