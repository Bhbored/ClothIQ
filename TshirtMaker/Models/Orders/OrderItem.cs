using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.Models.Orders
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        public Guid DesignId { get; set; }
        
        [ForeignKey("DesignId")]
        public virtual Design? Design { get; set; }

        [Required]
        public ClothingType ClothingType { get; set; }

        [Required]
        public ClothingSize Size { get; set; }

        [Required]
        public Material Material { get; set; }

        [Required]
        [MaxLength(7)]
        public string Color { get; set; } = "#FFFFFF";

        public PrintPosition PrintPosition { get; set; } = PrintPosition.Front;

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(1000)]
        public string? CustomizationNotes { get; set; }

        [MaxLength(50)]
        public string? ProductionStatus { get; set; }

        public DateTime? PrintedAt { get; set; }

        public DateTime? QualityCheckedAt { get; set; }

        [MaxLength(2048)]
        public string? MockupImageUrl { get; set; }

        [MaxLength(2048)]
        public string? PrintFileUrl { get; set; }
        public void CalculateTotalPrice()
        {
            TotalPrice = UnitPrice * Quantity;
        }

        public string GetDisplayName()
        {
            return $"{ClothingType} - {Size} - {Color}";
        }
    }
}
