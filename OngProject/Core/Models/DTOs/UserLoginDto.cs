using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(320, ErrorMessage = "The maximum length is 320 letters")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The maximum length is 20 letters")]
        public string Password { get; set; }

    }
}
