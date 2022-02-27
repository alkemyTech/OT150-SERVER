using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;
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
            var Members = _unitOfWork.MemberModelRepository.GetAll();
            
            var MembersDto = new List<MemberDto>();

            foreach (var AMember in Members)
            {
                MembersDto.Add(entityMapper.MemberListDtoMemberModel(AMember));
            }

            return MembersDto;
        }

    }
}
