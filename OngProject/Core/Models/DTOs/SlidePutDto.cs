using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class SlidePutDto
    {
        public IFormFile ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }
    }
}
