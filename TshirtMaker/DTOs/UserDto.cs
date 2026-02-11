using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;
using TshirtMaker.Utility;

namespace TshirtMaker.DTOs
{
    public class UserDto : BaseEntityDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = "https://api.dicebear.com/7.x/avataaars/svg?seed=";

        [JsonPropertyName("cover_image_url")]
        public string? CoverImageUrl { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("website_url")]
        public string? WebsiteUrl { get; set; }

        [JsonPropertyName("total_shares")]
        public int TotalShares { get; set; } = 0;

        [JsonPropertyName("following_count")]
        public int FollowingCount { get; set; } = 0;

        [JsonPropertyName("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        [JsonPropertyName("designs")]
        public List<Guid>? Designs { get; set; }

        [JsonPropertyName("posts")]
        public List<Guid>? Posts { get; set; }

        [JsonPropertyName("comments")]
        public List<Guid>? Comments { get; set; }

        [JsonPropertyName("bookmarks")]
        public List<Guid>? Bookmarks { get; set; }

        [JsonPropertyName("followers")]
        public List<Guid>? Followers { get; set; }

        [JsonPropertyName("notifications")]
        public List<Guid>? Notifications { get; set; }

       

        public User ToModel()
        {
            return new User
            {
                Id = this.Id,
                Username = this.Username,
                Email = this.Email,
                Bio = this.Bio,
                AvatarUrl = this.AvatarUrl,
                CoverImageUrl = this.CoverImageUrl,
                Location = this.Location,
                WebsiteUrl = this.WebsiteUrl,
                TotalShares = this.TotalShares,
                FollowingCount = this.FollowingCount,
                LastLoginAt = this.LastLoginAt,
                Designs = this.Designs?.Select(id => new Design { Id = id }).ToList() ?? new List<Design>(),
                Posts = this.Posts?.Select(id => new Models.Social.Post { Id = id }).ToList() ?? new List<Models.Social.Post>(),
                Comments = this.Comments?.Select(id => new Models.Social.Comment { Id = id }).ToList() ?? new List<Models.Social.Comment>(),
                Bookmarks = this.Bookmarks?.Select(id => new Models.Social.Bookmark { Id = id }).ToList() ?? new List<Models.Social.Bookmark>(),
                Followers = this.Followers?.Select(id => new Models.Social.Follower { Id = id }).ToList() ?? new List<Models.Social.Follower>(),
                Notifications = this.Notifications?.Select(id => new Models.Social.Notification { Id = id }).ToList() ?? new List<Models.Social.Notification>(),
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static UserDto FromModel(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl,
                CoverImageUrl = user.CoverImageUrl,
                Location = user.Location,
                WebsiteUrl = user.WebsiteUrl,
                TotalShares = user.TotalShares,
                FollowingCount = user.FollowingCount,
                LastLoginAt = user.LastLoginAt,
                Designs = user.Designs?.Select(d => d.Id).ToList() ,
                Posts = user.Posts?.Select(p => p.Id).ToList() ,
                Comments =  user.Comments?.Select(c => c.Id).ToList()       ,
                Bookmarks = user.Bookmarks?.Select(b => b.Id).ToList(),
                Followers = user.Followers?.Select(f => f.Id).ToList(),
                Notifications = user.Notifications?.Select(n => n.Id).ToList(),
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
