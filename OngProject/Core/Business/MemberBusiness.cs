using System.Collections.Generic;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMembers
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper;

        public MemberBusiness(IUnitOfWork UnitOfWork, EntityMapper EntityMapper)
        {
            _unitOfWork = UnitOfWork;
            entityMapper = EntityMapper;
        }
        public IEnumerable<MemberDto> GetMembers()
        {
            var members = _unitOfWork.MemberModelRepository.GetAll();
            var membersDto = new List<MemberDto>();

            foreach (var member in members)
            {
                membersDto.Add(entityMapper.MemberListDtoMemberModel(member));
            }
            
            return membersDto;
        }

    }
}
