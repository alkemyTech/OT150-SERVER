using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class SlidePostDto
    {
       
        [Required]
 
        public string Image{ get; set; }
        [Required]
        [MaxLength(255,ErrorMessage="255 is the maximum length.")]
        public string Text { get; set; }
     
        public int? Order { get; set; }
        [Required]
        public int OrganizationId { get; set; }
    }

}
