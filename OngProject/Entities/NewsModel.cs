﻿using OngProject.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
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

        [ForeignKey("CategorieModel")]
        public int Categorie_Id { get; set; }
        public virtual CategorieModel Categorie { get; set; }

    }
}
