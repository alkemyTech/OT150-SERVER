using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class ContactPostDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The maximum length is 255 letters")]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(320,ErrorMessage = "The maximum length is 320 letters")]
    
        public string Email { get; set; }
        [MaxLength(20, ErrorMessage = "The maximum length is 20 letters")]
        public string Phone { get; set; }
        [MaxLength(255, ErrorMessage = "The maximum length is 255 letters")]
        
        public string Message { get; set; }
    }
}
