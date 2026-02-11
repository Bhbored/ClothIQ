using System.ComponentModel.DataAnnotations;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.Models.Core
{
    public class StylePresetPreview : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public StylePresetType StylePreset { get; set; }

        [Required]
        [MaxLength(2048)]
        public string PreviewImageUrl { get; set; } = string.Empty;
    }
}
