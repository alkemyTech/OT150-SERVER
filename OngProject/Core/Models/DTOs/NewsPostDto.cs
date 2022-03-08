using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewsPostDto
    {
        [Required(ErrorMessage = "entered a name")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "entered content")]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int CategorieId { get; set; }
        public DateTime LastModified { get; }
        public bool SoftDelete { get; }

        public NewsPostDto()
        {
            LastModified = DateTime.Now;
            SoftDelete = true;
        }
    }
}
