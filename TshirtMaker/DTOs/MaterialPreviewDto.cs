using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;

namespace TshirtMaker.DTOs
{
    public class MaterialPreviewDto : BaseEntityDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("material")]
        public string Material { get; set; } = string.Empty;

        [JsonPropertyName("preview_image_url")]
        public string PreviewImageUrl { get; set; } = string.Empty;

        public MaterialPreview ToModel()
        {
            return new MaterialPreview
            {
                Id = this.Id,
                Title = this.Title,
                Material = this.Material,
                PreviewImageUrl = this.PreviewImageUrl,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static MaterialPreviewDto FromModel(MaterialPreview materialPreview)
        {
            return new MaterialPreviewDto
            {
                Id = materialPreview.Id,
                Title = materialPreview.Title,
                Material = materialPreview.Material,
                PreviewImageUrl = materialPreview.PreviewImageUrl,
                CreatedAt = materialPreview.CreatedAt,
                UpdatedAt = materialPreview.UpdatedAt
            };
        }
    }
}