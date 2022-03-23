using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryUpdateDto
    {
        public string NameCategorie { get; set; }
        public string DescriptionCategorie { get; set; }

        [ExtensionsValidationHelper((new string[] { ".jpg", ".jpeg", ".png" }))]
        public IFormFile Image { get; set; }
    }
}
