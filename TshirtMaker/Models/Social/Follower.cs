using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Social
{
    public class Follower : BaseEntity
    {
        [Required]
        public Guid FollowerId { get; set; }

        [Required]
        public Guid FollowingId { get; set; }

        public bool IsMutual { get; set; } = false;

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;

        public virtual User? FollowerUser { get; set; }
        public virtual User? FollowingUser { get; set; }
    }
}
