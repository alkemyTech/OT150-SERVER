using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public List<OrganizationGetDto> GetOrganization()
        {
            int max = (unitOfWork.OrganizationModelRepository.GetAll()).Max(i => i.Id);
            var organization = (unitOfWork.OrganizationModelRepository.GetAll()).First(a => a.Id == max);
            var organizationDto = new List<OrganizationGetDto>();
            organizationDto.Add(entityMapper.OrganizationModeltoOrganizationGetDto(organization));
            return organizationDto;
        }
    }
}
