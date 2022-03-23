using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UserLoginToDisplayDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "The maximum length is 255 letters")]

        public string Name { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "The maximum length is 255 letters")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(320, ErrorMessage = "The maximum length is 320 letters")]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
