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
        [Required]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(65535)]
        public string Content { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
    }
}
