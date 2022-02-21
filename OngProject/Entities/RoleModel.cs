using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class RoleModel : EntityBase
    {
      
        [Required]
       
        public string NameRole { get; set; }
        public string DescriptionRole { get; set; }


    }
}
