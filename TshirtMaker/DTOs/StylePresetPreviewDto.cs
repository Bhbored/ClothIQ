using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.DTOs
{
    public class StylePresetPreviewDto : BaseEntityDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("style_preset")]
        public StylePresetType StylePreset { get; set; }

        [JsonPropertyName("preview_image_url")]
        public string PreviewImageUrl { get; set; } = string.Empty;

        public StylePresetPreview ToModel()
        {
            return new StylePresetPreview
            {
                Id = this.Id,
                Title = this.Title,
                StylePreset = this.StylePreset,
                PreviewImageUrl = this.PreviewImageUrl,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static StylePresetPreviewDto FromModel(StylePresetPreview stylePresetPreview)
        {
            return new StylePresetPreviewDto
            {
                Id = stylePresetPreview.Id,
                Title = stylePresetPreview.Title,
                StylePreset = stylePresetPreview.StylePreset,
                PreviewImageUrl = stylePresetPreview.PreviewImageUrl,
                CreatedAt = stylePresetPreview.CreatedAt,
                UpdatedAt = stylePresetPreview.UpdatedAt
            };
        }
    }
}