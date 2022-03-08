﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMemberBusiness
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
            var error = new List<string>();
            if (member== null || member.SoftDelete==false)
            {
                error.Add("404");
                response.Data = null;
                response.Succeeded = false;
                response.Message = "This member not found";
                response.Errors = error.ToArray();
            }
            else
            {
                var memberDeleted = await _unitOfWork.MemberModelRepository.Delete(id);
                _unitOfWork.SaveChanges();
                error.Add("200");
                response.Errors = error.ToArray();
                response.Data = entityMapper.MemberModelToMemberDeleteDto(memberDeleted);
                response.Succeeded = true;
                response.Message = "The member has been successfully deleted";
            }
            return response;
        }
        public Response<MemberModel> PostMember(MemberCreateDto memberDto)
        {
            Response<MemberModel> response = new Response<MemberModel>();
            List<string> intermediate_list = new List<string>();
            var memberToCreate = entityMapper.MemberPostDtoToMemberModel(memberDto);
            _unitOfWork.MemberModelRepository.Add(memberToCreate);
            _unitOfWork.SaveChanges();
            intermediate_list.Add("200");
            response.Errors = intermediate_list.ToArray();
            response.Data = memberToCreate;
            response.Succeeded = true;
            response.Message = "The Member was Posted successfully";
            return response;

        }
    }
}
