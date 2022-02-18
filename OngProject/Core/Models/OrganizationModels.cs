namespace OngProject.Core.Models
{
    public class OrganizationModels
    {
        [Required(ErrorMessage = "Entered a name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public int Phone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Entered text")]
        [Display(Name = "Welcome Text")]
        public string WelcomeText { get; set; }

        [Required(ErrorMessage = "Entered text")]
        [Display(Name = "About Us Text")]
        public string AboutUsText { get; set; }
    }
}
