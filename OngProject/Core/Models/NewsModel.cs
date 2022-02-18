using OngProject.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models
{
    public class NewsModel : EntityBase
    {
        [Required(ErrorMessage = "entered a name")]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "entered content")]
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }

        [ForeignKey("CategoriesModels")]
        public int Categories_Id { get; set; }
        public virtual CategoriesModels Categories { get; set; }

    }
}
//para 20:16 pa pa' comer volvi a las 21:01 -termine los models a las 22:02