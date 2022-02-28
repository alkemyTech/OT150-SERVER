using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{

    public class CategorieController : Controller
    {
        private readonly ICategorieBussines _categorieBusiness;

        public CategorieController(ICategorieBussines categorieBussines)
        {
            _categorieBusiness = categorieBussines;
        }

        [HttpGet("ListaCategorias")]
        public async Task<IActionResult> ListaCategorias()
        {

            try
            {

                return Ok(_categorieBusiness.GetCategories());

            }

            catch
            {
                return BadRequest();
            }
        }
    }
}
