using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.DTOs
{
    public class DesignDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        [JsonPropertyName("negative_prompt")]
        public string? NegativePrompt { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("style_preset")]
        public StylePresetType? StylePreset { get; set; }

        [JsonPropertyName("resolution")]
        public Resolution Resolution { get; set; } = Resolution.Standard;

        [JsonPropertyName("clothing_type")]
        public ClothingType ClothingType { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = "#FFFFFF";

        [JsonPropertyName("size")]
        public ClothingSize Size { get; set; }

        [JsonPropertyName("material")]
        public Material Material { get; set; }

        [JsonPropertyName("print_position")]
        public PrintPosition PrintPosition { get; set; } = PrintPosition.Front;

        [JsonPropertyName("final_image_url")]
        public string FinalImageUrl { get; set; } = string.Empty;

        // Navigation property stored as JSON
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        public Design ToModel()
        {
            return new Design
            {
                Id = this.Id,
                UserId = this.UserId,
                Prompt = this.Prompt,
                NegativePrompt = this.NegativePrompt,
                Title = this.Title,
                StylePreset = this.StylePreset,
                Resolution = this.Resolution,
                ClothingType = this.ClothingType,
                Color = this.Color,
                Size = this.Size,
                Material = this.Material,
                PrintPosition = this.PrintPosition,
                FinalImageUrl = this.FinalImageUrl,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static DesignDto FromModel(Design design)
        {
            return new DesignDto
            {
                Id = design.Id,
                UserId = design.UserId,
                Prompt = design.Prompt,
                NegativePrompt = design.NegativePrompt,
                Title = design.Title,
                StylePreset = design.StylePreset,
                Resolution = design.Resolution,
                ClothingType = design.ClothingType,
                Color = design.Color,
                Size = design.Size,
                Material = design.Material,
                PrintPosition = design.PrintPosition,
                FinalImageUrl = design.FinalImageUrl,
                User = design.User?.Id,
                CreatedAt = design.CreatedAt,
                UpdatedAt = design.UpdatedAt
            };
        }
    }
}