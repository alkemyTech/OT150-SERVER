using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class MemberCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public bool SoftDelete { get; set; }

        public MemberCreateDto()
        {
            LastModified = DateTime.Now;
            SoftDelete = true;
        }
    }
}
