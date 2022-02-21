using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities.Jwt
{
    public class RequestToken
    {
        [Required]
        public string TokenCode { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

    }
}
