using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationPutDto
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        [MaxLength(320)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string WelcomeText { get; set; }
        [MaxLength(2000)]
        public string AboutUsText { get; set; }
        [MaxLength(255)]
        public string FacebooK { get; set; }
        [MaxLength(255)]
        public string Linkedin { get; set; }
        [MaxLength(255)]
        public string Instagram { get; set; }
    }
}
