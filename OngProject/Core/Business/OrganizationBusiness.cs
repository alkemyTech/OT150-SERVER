using OngProject.Core.Interfaces;
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
        private readonly ISlideBusiness _slideBusiness;
        
        public OrganizationBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper, ISlideBusiness slideBusiness)
        {
            this.unitOfWork = unitOfWork;
            this.entityMapper = entityMapper;
            this._slideBusiness = slideBusiness;
        }

        public async Task<IEnumerable<OrganizationGetDto>> GetOrganization()
        {


            var organizations =  unitOfWork.OrganizationModelRepository.GetAll();
            
            if (organizations.Any(x=>x.SoftDelete=true))
            {
                var slides = unitOfWork.SlideModelRepository.GetAll().OrderBy(x => x.Order);
                var slidesDto = new List<SlideDto>();
                foreach (var slide in slides)
                {
                    slidesDto.Add(entityMapper.SlideModelToSlideDto(slide));
                }

                List<OrganizationGetDto> organizationDtoToDisplay = new();

                foreach (var entity in organizations)
                {
                  
                    var orgDto = entityMapper.OrganizationModeltoOrganizationGetDto(entity);
                    var slidesAux = slidesDto.Where(x => x.OrganizationId == entity.Id);
                    var slidesAux2 = new List<SlideDtoToDisplay>();
                    foreach (var slideAux in slidesAux)
                    {
                        slidesAux2.Add(entityMapper.SlideDtoToSlideDtoToDisplay(slideAux));
                    }
                    orgDto.Slides= slidesAux2;
                
                  
                    organizationDtoToDisplay.Add(orgDto);
                }

                return organizationDtoToDisplay;

            }
            else
            {
                return null;
            }
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
