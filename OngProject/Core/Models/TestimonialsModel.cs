using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models
    {
        public class TestimonialsModel
        {
            [Key]
            public int Id { get; set; }
            [MaxLength(255)]
            [Required]
            public string Name { get; set; }
            [MaxLength(65535)]
            public string Content { get; set; }
            [MaxLength(255)]
            public string Image { get; set; }

            [DataType(DataType.Date)]
            public DateTime DeletedAt { get; set; }
        }
    }


