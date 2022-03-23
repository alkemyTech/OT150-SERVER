using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class OrganizationModel : EntityBase
    {
        [Required(ErrorMessage = "Entered a name")]
        [Display(Name = "Name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(20)]
        public int Phone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Entered text")]
        [Display(Name = "Welcome Text")]
        [MaxLength(500)]
        public string WelcomeText { get; set; }

        [Required(ErrorMessage = "Entered text")]
        [Display(Name = "About Us Text")]
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
