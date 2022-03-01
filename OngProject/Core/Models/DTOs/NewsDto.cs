using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewsDto
    {
        [Required(ErrorMessage = "entered a name")]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "entered content")]
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
