using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class CommentModel : EntityBase
    {
        [Required]
        [MaxLength(65535)]
        public string Body { get; set; }

        [ForeignKey("NewsModel")]
        public int NewsId { get; set; }
        public virtual NewsModel News { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public virtual UserModel User { get; set; }
    }
}
