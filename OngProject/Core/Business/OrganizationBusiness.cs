using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
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
    }
}
