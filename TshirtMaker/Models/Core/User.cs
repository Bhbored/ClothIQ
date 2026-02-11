using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Social;

namespace TshirtMaker.Models.Core
{
    public class User : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        
        public string? PasswordHash { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(2048)]
        public string AvatarUrl { get; set; } = "https://api.dicebear.com/7.x/avataaars/svg?seed=";

        [MaxLength(2048)]
        public string? CoverImageUrl { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(2048)]
        public string? WebsiteUrl { get; set; }

        public int TotalDesigns => Designs.Count;
        public int TotalLikes => LikesTaken.Count;
        public int TotalLikesGiven => LikesGiven.Count;
        public int TotalPosts => Posts.Count;
        public int TotalCollection => Collections.Count;
        public int TotalShares { get; set; } = 0;

        public int FollowersCount
        {
            get
            {
                return Followers
                    .ToList().Count;
            }
        }

        public int FollowingCount { get; set; } = 0;

        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<Design> Designs { get; set; } = [];
        public virtual ICollection<Post> Posts { get; set; } = [];
        public virtual ICollection<Like> LikesGiven { get; set; } = [];
        public virtual ICollection<Like> LikesTaken { get; set; } = [];

        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<Bookmark> Bookmarks { get; set; } = [];
        public virtual ICollection<Follower> Followers { get; set; } = [];
        public virtual ICollection<Notification> Notifications { get; set; } = [];
        public virtual ICollection<Collection> Collections { get; set; } = [];
    }
}

