using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MemberBusiness : IMembers
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper;
        private readonly IConfiguration _configuration;
        private readonly ImagesBusiness _imagesBusiness;

        public MemberBusiness(IUnitOfWork UnitOfWork, EntityMapper EntityMapper, ImagesBusiness imagesBusiness, IConfiguration configuration)
        {
            _unitOfWork = UnitOfWork;
            entityMapper = EntityMapper;
            _imagesBusiness = imagesBusiness;
            _configuration = configuration;
        }

        public Response<MemberDto> Create(MemberDto memberDto)
        {

            var response = new Response<MemberDto>();
            var member = entityMapper.MemberDtoToMemberModel(memberDto);

            _unitOfWork.MemberModelRepository.Add(member);
            _unitOfWork.SaveChanges();

            response.Data = memberDto;
            response.Succeeded = true;
            response.Message = "The member was successfully added";
            return response;

        }

        public PagedList<MemberDto> GetMembers(PaginationParams paginationParams)
        {
            var members = _unitOfWork.MemberModelRepository.GetAll();
            var membersDto = new List<MemberDto>();

            foreach (var member in members)
            {
                membersDto.Add(entityMapper.MemberListDtoMemberModel(member));
            }

            var pagedMembers = PagedList<MemberDto>.Create(membersDto, paginationParams.PageNumber, paginationParams.PageSize);


            return pagedMembers;
        }

        public async Task<Response<MemberDto>> Update(int id, MemberPutDto memberPutDto)
        {
            var imagesBussines = new ImagesBusiness(_configuration);
            var response = new Response<MemberDto>();
            var errorList = new List<string>();
            string image;
            var memberUpdate = _unitOfWork.MemberModelRepository.GetById(id);

            if (memberUpdate == null)
            {
                errorList.Add("Member not found");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }
            if (memberPutDto.Image != null)
            {
                image = await imagesBussines.UploadFileAsync(memberPutDto.Image);
                memberUpdate.Image = image;
            }

            memberUpdate.Name = memberPutDto.Name;
            memberUpdate.FacebookUrl = memberPutDto.FacebookUrl;
            memberUpdate.InstagramUrl = memberPutDto.InstagramUrl;
            memberUpdate.LinkedinUrl = memberPutDto.LinkedinUrl;
            memberUpdate.Description = memberPutDto.Description;

            _unitOfWork.MemberModelRepository.Update(memberUpdate);
            await _unitOfWork.SaveChangesAsync();

            response.Data = entityMapper.MemberModelToMemberPutDto(memberUpdate);
            response.Succeeded = true;
            return response;
        }

        public async Task<Response<MemberDeleteDto>> Delete(int id)
        {
            var members = _unitOfWork.MemberModelRepository.GetAll();
   
            Response<MemberDeleteDto> response = new Response<MemberDeleteDto>();

            var member = members.FirstOrDefault(x=>x.Id==id);
            var error = new List<string>();
            if (member== null || member.SoftDelete==false)
            {
                
                error.Add("This member not found");
                response.Data = null;
                response.Succeeded = false;
                response.Message = "The deletion was not successfully";
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
