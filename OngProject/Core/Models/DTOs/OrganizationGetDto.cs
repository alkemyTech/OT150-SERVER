using System.Collections.Generic;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationGetDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        
        public int Phone { get; set; }
        public IEnumerable<SlideDtoToDisplay> Slides { get; set; }
    }
}
