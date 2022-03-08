using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonialsPostDto
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(65535)]
        public string Content { get; set; }
       
        [ExtensionsValidationHelper((new string[] { ".jpg", ".jpeg", ".png" }))]
        public IFormFile Image { get; set; }
    }
}
