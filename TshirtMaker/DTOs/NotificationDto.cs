using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;
using TshirtMaker.Models.Social;

namespace TshirtMaker.DTOs
{
    public class NotificationDto : BaseEntityDto
    {
        [JsonPropertyName("recipient_id")]
        public Guid RecipientId { get; set; }

        [JsonPropertyName("sender_id")]
        public Guid SenderId { get; set; }

        [JsonPropertyName("type")]
        public NotificationType Type { get; set; }

        [JsonPropertyName("post_id")]
        public Guid? PostId { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("is_read")]
        public bool IsRead { get; set; } = false;

        [JsonPropertyName("action_taken")]
        public bool ActionTaken { get; set; } = false;

        [JsonPropertyName("notified_at")]
        public DateTime NotifiedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("recipient_ref")]
        public Guid? Recipient { get; set; }

        [JsonPropertyName("sender_ref")]
        public Guid? Sender { get; set; }

        [JsonPropertyName("post_ref")]
        public Guid? Post { get; set; }

        public Notification ToModel()
        {
            return new Notification
            {
                Id = this.Id,
                RecipientId = this.RecipientId,
                SenderId = this.SenderId,
                Type = this.Type,
                PostId = this.PostId,
                Message = this.Message,
                IsRead = this.IsRead,
                ActionTaken = this.ActionTaken,
                NotifiedAt = this.NotifiedAt,
                Recipient = this.Recipient.HasValue ? new User { Id = this.Recipient.Value } : null,
                Sender = this.Sender.HasValue ? new User { Id = this.Sender.Value } : null,
                Post = this.Post.HasValue ? new Post { Id = this.Post.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static NotificationDto FromModel(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                RecipientId = notification.RecipientId,
                SenderId = notification.SenderId,
                Type = notification.Type,
                PostId = notification.PostId,
                Message = notification.Message,
                IsRead = notification.IsRead,
                ActionTaken = notification.ActionTaken,
                NotifiedAt = notification.NotifiedAt,
                Recipient = notification.Recipient?.Id,
                Sender = notification.Sender?.Id,
                Post = notification.Post?.Id,
                CreatedAt = notification.CreatedAt,
                UpdatedAt = notification.UpdatedAt
            };
        }
    }
}