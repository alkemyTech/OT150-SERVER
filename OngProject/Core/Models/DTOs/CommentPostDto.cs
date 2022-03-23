using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CommentPostDto
    {
        [Required]
        [MaxLength(65535)]
        public string Body { get; set; }
        
        [Required]
        public int NewsId { get; set; }
    }
}
