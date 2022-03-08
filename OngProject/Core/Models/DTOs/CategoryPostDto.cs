using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryPostDto
    {
        [Required]
        [MaxLength(255)]
        public string NameCategory { get; set; }
        [Required]
        [MaxLength(255)]
        public string DescriptionCategory { get; set; }
        [Required]
        [MaxLength(255)]
        public string Image { get; set; }
        public DateTime LastModified { get; }
        public bool SoftDelete { get; }

        public CategoryPostDto()
        {
            LastModified = DateTime.Now;
            SoftDelete = true;
        }
    }
}
