using Microsoft.AspNetCore.Mvc;
using OngProject.Repositories.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategorieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
