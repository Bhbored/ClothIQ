using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class LikeDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("post_id")]
        public Guid PostId { get; set; }

        [JsonPropertyName("liked_at")]
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties stored as JSON references
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        [JsonPropertyName("post_ref")]
        public Guid? Post { get; set; }

        public Like ToModel()
        {
            return new Like
            {
                Id = this.Id,
                UserId = this.UserId,
                PostId = this.PostId,
                LikedAt = this.LikedAt,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                Post = this.Post.HasValue ? new Post { Id = this.Post.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static LikeDto FromModel(Like like)
        {
            return new LikeDto
            {
                Id = like.Id,
                UserId = like.UserId,
                PostId = like.PostId,
                LikedAt = like.LikedAt,
                User = like.User?.Id,
                Post = like.Post?.Id,
                CreatedAt = like.CreatedAt,
                UpdatedAt = like.UpdatedAt
            };
        }
    }
}