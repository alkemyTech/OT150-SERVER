using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;

namespace OngProject.Core.Models.DTOs
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [ExtensionsValidationHelper((new string[] { ".jpg", ".jpeg", ".png" }))]
        public IFormFile Photo { get; set; }
    }
}
