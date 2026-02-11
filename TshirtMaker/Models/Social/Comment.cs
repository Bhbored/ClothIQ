using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Social
{
    public class Comment : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PostId { get; set; }


        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
