using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
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
        public async Task<Response<MemberDeleteDto>> Delete(int id)
        {
            var members = _unitOfWork.MemberModelRepository.GetAll();
   
            Response<MemberDeleteDto> response = new Response<MemberDeleteDto>();

            var member = members.FirstOrDefault(x=>x.Id==id);

            if (member== null || member.SoftDelete==false)
            {
                var error = new List<string>();
                error.Add("The member does not exist");
                response.Data = null;
                response.Succeeded = false;
                response.Message = "The member has not been successfully deleted";
                response.Errors = error.ToArray();

            }
            else
            {
                var memberDeleted = await _unitOfWork.MemberModelRepository.Delete(id);
                _unitOfWork.SaveChanges();
                response.Data = entityMapper.MemberModelToMemberDeleteDto(memberDeleted);
                response.Succeeded = true;
                response.Message = "The member has been successfully deleted";

            }
           
            
            return response;
            



         


        }

    }
}
