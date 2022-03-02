using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class CategorieDto
    {
        [Required]
        public string NameCategorie { get; set; }
    }
}
