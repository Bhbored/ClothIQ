using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.Models.Social
{
    public class Notification : BaseEntity
    {
        [Required]
        public Guid RecipientId { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public Guid? PostId { get; set; }

        [MaxLength(500)]
        public string? Message { get; set; }

        public bool IsRead { get; set; } = false;

        public bool ActionTaken { get; set; } = false;

        public DateTime NotifiedAt { get; set; } = DateTime.UtcNow;

        public virtual User? Recipient { get; set; }
        public virtual User? Sender { get; set; }
        public virtual Post? Post { get; set; }
    }
}
