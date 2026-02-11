using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class FollowerDto : BaseEntityDto
    {
        [JsonPropertyName("follower_id")]
        public Guid FollowerId { get; set; }

        [JsonPropertyName("following_id")]
        public Guid FollowingId { get; set; }

        [JsonPropertyName("is_mutual")]
        public bool IsMutual { get; set; } = false;

        [JsonPropertyName("followed_at")]
        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("follower_user_ref")]
        public Guid? FollowerUser { get; set; }

        [JsonPropertyName("following_user_ref")]
        public Guid? FollowingUser { get; set; }

        public Follower ToModel()
        {
            return new Follower
            {
                Id = this.Id,
                FollowerId = this.FollowerId,
                FollowingId = this.FollowingId,
                IsMutual = this.IsMutual,
                FollowedAt = this.FollowedAt,
                FollowerUser = this.FollowerUser.HasValue ? new User { Id = this.FollowerUser.Value } : null,
                FollowingUser = this.FollowingUser.HasValue ? new User { Id = this.FollowingUser.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static FollowerDto FromModel(Follower follower)
        {
            return new FollowerDto
            {
                Id = follower.Id,
                FollowerId = follower.FollowerId,
                FollowingId = follower.FollowingId,
                IsMutual = follower.IsMutual,
                FollowedAt = follower.FollowedAt,
                FollowerUser = follower.FollowerUser?.Id,
                FollowingUser = follower.FollowingUser?.Id,
                CreatedAt = follower.CreatedAt,
                UpdatedAt = follower.UpdatedAt
            };
        }
    }
}