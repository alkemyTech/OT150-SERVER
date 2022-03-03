using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class SlideModel : EntityBase
    {
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        
        [ForeignKey("OrganizationModel")]
        public int OrganizationId { get; set; }
        public virtual OrganizationModel Organization { get; set; }

    }
}
