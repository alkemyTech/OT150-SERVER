using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrganizationBusiness organizationBusiness;

        public OrganizationController(IUnitOfWork unitOfWork, OrganizationBusiness organizationBusiness)
        {
            _unitOfWork = unitOfWork;
            this.organizationBusiness = organizationBusiness;
        }

        [HttpGet("public")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(organizationBusiness.GetOrganization());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("public")]
        public IActionResult Put(OrganizationPutDto organizationPutDto)
        {
            return Ok(organizationBusiness.PutOrganization(organizationPutDto));

        }
    }
}
