using System.ComponentModel.DataAnnotations;

namespace TshirtMaker.Models.Core
{
    public class MaterialPreview : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Material { get; set; } = string.Empty;

        [Required]
        [MaxLength(2048)]
        public string PreviewImageUrl { get; set; } = string.Empty;
    }
}
