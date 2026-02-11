using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;

namespace TshirtMaker.DTOs
{
    public class CollectionDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("copied_design_id")]
        public Guid CopiedDesignId { get; set; }

        // Navigation properties stored as JSON
        [JsonPropertyName("copied_design_ref")]
        public Guid? CopiedDesign { get; set; }

        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        public Collection ToModel()
        {
            return new Collection
            {
                Id = this.Id,
                UserId = this.UserId,
                CopiedDesignId = this.CopiedDesignId,
                CopiedDesign = this.CopiedDesign.HasValue ? new Design { Id = this.CopiedDesign.Value } : null,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static CollectionDto FromModel(Collection collection)
        {
            return new CollectionDto
            {
                Id = collection.Id,
                UserId = collection.UserId,
                CopiedDesignId = collection.CopiedDesignId,
                CopiedDesign = collection.CopiedDesign?.Id,
                User = collection.User?.Id,
                CreatedAt = collection.CreatedAt,
                UpdatedAt = collection.UpdatedAt
            };
        }
    }
}