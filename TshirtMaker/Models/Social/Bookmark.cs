using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Social
{
    public class Bookmark : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PostId { get; set; }

        public DateTime BookmarkedAt { get; set; } = DateTime.UtcNow;

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
