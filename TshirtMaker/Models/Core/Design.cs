using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TshirtMaker.Models.Enums;
using TshirtMaker.Utility;

namespace TshirtMaker.Models.Core
{
    public class Design : BaseEntity
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Prompt { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? NegativePrompt { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        public StylePresetType? StylePreset { get; set; }

        public Resolution Resolution { get; set; } = Resolution.Standard;

        [Required]
        public ClothingType ClothingType { get; set; }

        [Required]
        public string Color { get; set; } = "#FFFFFF";

        public string? DisplayedColor => ColorHelper.HexToColorName(Color);

        [Required]
        public ClothingSize Size { get; set; }

        [Required]
        public Material Material { get; set; }

        public PrintPosition PrintPosition { get; set; } = PrintPosition.Front;

        [MaxLength(2048)]
        public string FinalImageUrl { get; set; } = string.Empty;

        public decimal Price => PriceCalculator.CalculatePrice(ClothingType, Material);

        public virtual User? User { get; set; }

    }
}

