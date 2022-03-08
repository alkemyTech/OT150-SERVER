using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class OrganizationBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EntityMapper entityMapper;
        
        public OrganizationBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper)
        {
            this.unitOfWork = unitOfWork;
            this.entityMapper = entityMapper;
        }

        public OrganizationGetDto GetOrganization()
        {
            var organizationlist = unitOfWork.OrganizationModelRepository.GetAll();
            OrganizationModel organization = new OrganizationModel();
            var organizationDto = new OrganizationGetDto();
            if (organizationlist.Any()){
                int max = organizationlist.Max(i => i.Id);
                organization = organizationlist.First(a => a.Id == max);
                organizationDto = entityMapper.OrganizationModeltoOrganizationGetDto(organization);
            }
            return organizationDto;
	    }
        
        public Response<OrganizationModel> PutOrganization(OrganizationPutDto organizationPut)
        {
            Response<OrganizationModel> response = new Response<OrganizationModel>();
            List<string> intermediate_list = new List<string>();
            var organization = unitOfWork.OrganizationModelRepository.GetById(organizationPut.Id);
            if (organization == null)
            {
                intermediate_list.Add("404");
                response.Data = organization;
                response.Message = "This OrganizationId not Found";
                response.Succeeded = false;
                response.Errors = intermediate_list.ToArray();
                return response;
            }
            if (organizationPut.Name != null)
                organization.Name = organizationPut.Name;
            if (organizationPut.Image != null)
                organization.Image = organizationPut.Image;
            if (organizationPut.Address != null)
                organization.Address = organizationPut.Address;
            if (organizationPut.Phone != 0)
                organization.Phone = organizationPut.Phone;
            if (organizationPut.Email != null)
                organization.Email = organizationPut.Email;
            if (organizationPut.WelcomeText != null)
                organization.WelcomeText = organizationPut.WelcomeText;
            if (organizationPut.AboutUsText != null)
                organization.AboutUsText = organizationPut.AboutUsText;
            if (organizationPut.FacebooK != null)
                organization.FacebooK = organizationPut.FacebooK;
            if (organizationPut.Instagram != null)
                organization.Instagram = organizationPut.Instagram;
            if (organizationPut.Linkedin != null)
                organization.Linkedin = organizationPut.Linkedin;
            organization.LastModified = DateTime.Now;
            organization = unitOfWork.OrganizationModelRepository.GetById(organizationPut.Id);
            intermediate_list.Add("200");
            response.Data = organization;
            response.Message = "The Organization was successfully Updated";
            response.Succeeded = true;
            response.Errors = intermediate_list.ToArray();
            return response;
        }
    }
}
