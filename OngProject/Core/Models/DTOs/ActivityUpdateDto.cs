using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class ActivityUpdateDto
    {
       
        [MaxLength(255)]
        public string Name { get; set; }
      
        [MaxLength(65535)]
        public string Content { get; set; }
        

  
        [ExtensionsValidationHelper((new string[] { ".jpg", ".jpeg", ".png" }))]
        public IFormFile Image { get; set; }
    }
}
