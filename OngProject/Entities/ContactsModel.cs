using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class ContactsModel : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(320)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(255)]
        [Required]
        public string Message { get; set; }



    }
}
