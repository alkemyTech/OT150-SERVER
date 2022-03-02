using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Business;
using OngProject.Repositories.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Get()
        {
            return Ok(organizationBusiness.GetOrganization());
        }
    }
}
