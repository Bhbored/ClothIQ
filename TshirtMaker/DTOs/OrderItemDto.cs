using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;
using TshirtMaker.Models.Orders;

namespace TshirtMaker.DTOs
{
    public class OrderItemDto : BaseEntityDto
    {
        [JsonPropertyName("order_id")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("design_id")]
        public Guid DesignId { get; set; }

        [JsonPropertyName("clothing_type")]
        public ClothingType ClothingType { get; set; }

        [JsonPropertyName("size")]
        public ClothingSize Size { get; set; }

        [JsonPropertyName("material")]
        public Material Material { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = "#FFFFFF";

        [JsonPropertyName("print_position")]
        public PrintPosition PrintPosition { get; set; } = PrintPosition.Front;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("customization_notes")]
        public string? CustomizationNotes { get; set; }

        [JsonPropertyName("production_status")]
        public string? ProductionStatus { get; set; }

        [JsonPropertyName("printed_at")]
        public DateTime? PrintedAt { get; set; }

        [JsonPropertyName("quality_checked_at")]
        public DateTime? QualityCheckedAt { get; set; }

        [JsonPropertyName("mockup_image_url")]
        public string? MockupImageUrl { get; set; }

        [JsonPropertyName("print_file_url")]
        public string? PrintFileUrl { get; set; }

        // Navigation properties stored as JSON references
        [JsonPropertyName("order_ref")]
        public Guid? Order { get; set; }

        [JsonPropertyName("design_ref")]
        public Guid? Design { get; set; }

        public OrderItem ToModel()
        {
            return new OrderItem
            {
                Id = this.Id,
                OrderId = this.OrderId,
                DesignId = this.DesignId,
                ClothingType = this.ClothingType,
                Size = this.Size,
                Material = this.Material,
                Color = this.Color,
                PrintPosition = this.PrintPosition,
                Quantity = this.Quantity,
                UnitPrice = this.UnitPrice,
                TotalPrice = this.TotalPrice,
                CustomizationNotes = this.CustomizationNotes,
                ProductionStatus = this.ProductionStatus,
                PrintedAt = this.PrintedAt,
                QualityCheckedAt = this.QualityCheckedAt,
                MockupImageUrl = this.MockupImageUrl,
                PrintFileUrl = this.PrintFileUrl,
                Order = this.Order.HasValue ? new Order { Id = this.Order.Value } : null,
                Design = this.Design.HasValue ? new Design { Id = this.Design.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static OrderItemDto FromModel(OrderItem orderItem)
        {
            return new OrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                DesignId = orderItem.DesignId,
                ClothingType = orderItem.ClothingType,
                Size = orderItem.Size,
                Material = orderItem.Material,
                Color = orderItem.Color,
                PrintPosition = orderItem.PrintPosition,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice,
                CustomizationNotes = orderItem.CustomizationNotes,
                ProductionStatus = orderItem.ProductionStatus,
                PrintedAt = orderItem.PrintedAt,
                QualityCheckedAt = orderItem.QualityCheckedAt,
                MockupImageUrl = orderItem.MockupImageUrl,
                PrintFileUrl = orderItem.PrintFileUrl,
                Order = orderItem.Order?.Id,
                Design = orderItem.Design?.Id,
                CreatedAt = orderItem.CreatedAt,
                UpdatedAt = orderItem.UpdatedAt
            };
        }
    }
}