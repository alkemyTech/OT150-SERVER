using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonialsPutDto
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
