using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class SlidePostDto
    {
        [Required]
        [MaxLength(32767, ErrorMessage = "32767 is the maximum length.")]
        public string Image{ get; set; }
        [Required]
        [MaxLength(255,ErrorMessage="255 is the maximum length.")]
        public string Text { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int OrganizationId { get; set; }
    }

}
