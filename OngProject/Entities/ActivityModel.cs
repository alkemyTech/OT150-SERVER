using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class ActivityModel : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(65535)]
        public string Content { get; set; }
        [Required]
        [MaxLength(255)]
        public string Image { get; set; }
    }
}
