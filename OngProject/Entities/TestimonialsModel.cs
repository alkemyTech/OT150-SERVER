using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class TestimonialsModel: EntityBase
    {
       
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [MaxLength(65535)]
        public string Content { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }


    }
}
