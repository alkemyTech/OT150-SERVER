using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;

namespace OngProject.Core.Models.DTOs
{

    public class UserRegisterDto
    {
  
        [Required]
        [MaxLength(255,ErrorMessage ="The maximum length is 255 letters")]
        
        public string Name { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "The maximum length is 255 letters")]
    
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
       
        [MaxLength(320, ErrorMessage = "The maximum length is 320 letters")]
        public string Email { get; set;}

        [ExtensionsValidationHelper((new string[] { ".jpg", ".jpeg", ".png" }))]
        public IFormFile Photo { get; set; }
       
        
      
        
        [Required]
        [MaxLength(20, ErrorMessage = "The maximum length is 20 letters")]
        public string Password { get; set; }
        [Required]

        [Range(1, 2,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]

        public int Role{ get; set; }

      
    }
}
