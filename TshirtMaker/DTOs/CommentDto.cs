using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class CommentDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("post_id")]
        public Guid PostId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        // Navigation properties stored as JSON references
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        [JsonPropertyName("post_ref")]
        public Guid? Post { get; set; }

        public Comment ToModel()
        {
            return new Comment
            {
                Id = this.Id,
                UserId = this.UserId,
                PostId = this.PostId,
                Text = this.Text,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                Post = this.Post.HasValue ? new Post { Id = this.Post.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static CommentDto FromModel(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.Text,
                User = comment.User?.Id,
                Post = comment.Post?.Id,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt
            };
        }
    }
}