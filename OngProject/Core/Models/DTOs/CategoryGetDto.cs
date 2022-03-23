using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryGetDto
    {
        [Required]
        public string NameCategorie { get; set; }
        [Required]
        public string DescriptionCategorie { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
