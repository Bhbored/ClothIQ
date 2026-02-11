using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class PostDto : BaseEntityDto
    {
        [JsonPropertyName("poster_id")]
        public Guid PosterId { get; set; }

        [JsonPropertyName("design_id")]
        public Guid DesignId { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("remix_count")]
        public int RemixCount { get; set; } = 0;

        [JsonPropertyName("allow_remix")]
        public bool AllowRemix { get; set; } = true;

        // Navigation properties stored as JSON arrays
        [JsonPropertyName("likes")]
        public List<Guid>? Likes { get; set; }

        [JsonPropertyName("comments")]
        public List<Guid>? Comments { get; set; }

        [JsonPropertyName("bookmarks")]
        public List<Guid>? Bookmarks { get; set; }

        // Navigation properties stored as JSON references
        [JsonPropertyName("poster_ref")]
        public Guid? Poster { get; set; }

        [JsonPropertyName("design_ref")]
        public Guid? Design { get; set; }

        public Post ToModel()
        {
            return new Post
            {
                Id = this.Id,
                PosterId = this.PosterId,
                DesignId = this.DesignId,
                Description = this.Description,
                RemixCount = this.RemixCount,
                AllowRemix = this.AllowRemix,
                Likes = this.Likes?.Select(id => new Like { Id = id }).ToList() ?? new List<Like>(),
                Comments = this.Comments?.Select(id => new Comment { Id = id }).ToList() ?? new List<Comment>(),
                Bookmarks = this.Bookmarks?.Select(id => new Bookmark { Id = id }).ToList() ?? new List<Bookmark>(),
                Poster = this.Poster.HasValue ? new User { Id = this.Poster.Value } : null,
                Design = this.Design.HasValue ? new Design { Id = this.Design.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static PostDto FromModel(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                PosterId = post.PosterId,
                DesignId = post.DesignId,
                Description = post.Description,
                RemixCount = post.RemixCount,
                AllowRemix = post.AllowRemix,
                Likes = post.Likes?.Select(l => l.Id).ToList(),
                Comments = post.Comments?.Select(c => c.Id).ToList(),
                Bookmarks = post.Bookmarks?.Select(b => b.Id).ToList(),
                Poster = post.Poster?.Id,
                Design = post.Design?.Id,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };
        }
    }
}