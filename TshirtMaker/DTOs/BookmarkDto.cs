using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class BookmarkDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("post_id")]
        public Guid PostId { get; set; }

        [JsonPropertyName("bookmarked_at")]
        public DateTime BookmarkedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties stored as JSON references
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        [JsonPropertyName("post_ref")]
        public Guid? Post { get; set; }

        public Bookmark ToModel()
        {
            return new Bookmark
            {
                Id = this.Id,
                UserId = this.UserId,
                PostId = this.PostId,
                BookmarkedAt = this.BookmarkedAt,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                Post = this.Post.HasValue ? new Post { Id = this.Post.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static BookmarkDto FromModel(Bookmark bookmark)
        {
            return new BookmarkDto
            {
                Id = bookmark.Id,
                UserId = bookmark.UserId,
                PostId = bookmark.PostId,
                BookmarkedAt = bookmark.BookmarkedAt,
                User = bookmark.User?.Id,
                Post = bookmark.Post?.Id,
                CreatedAt = bookmark.CreatedAt,
                UpdatedAt = bookmark.UpdatedAt
            };
        }
    }
}