using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class MemberDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Url]
        public string FacebookUrl { get; set; }

        [Url]
        public string InstagramUrl { get; set; }

        [Url]
        public string LinkedinUrl { get; set; }

        
        public string Image { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
