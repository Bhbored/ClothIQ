using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;

namespace TshirtMaker.Models.Orders
{
    public class Order : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int ProgressStep { get; set; } = 1;

        public virtual ICollection<OrderItem>? Items { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ShippingCost { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Tax { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Discount { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; } = "USD";

        public Guid? ShippingAddressId { get; set; }
        
        [ForeignKey("ShippingAddressId")]
        public virtual ShippingAddress? ShippingAddress { get; set; }

        [MaxLength(100)]
        public string? ShippingMethod { get; set; }

        [MaxLength(100)]
        public string? CarrierName { get; set; }

        [MaxLength(100)]
        public string? TrackingNumber { get; set; }

        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? ConfirmedAt { get; set; }
        
        public DateTime? ProductionStartedAt { get; set; }
        
        public DateTime? ShippedAt { get; set; }
        
        public DateTime? DeliveredAt { get; set; }
        
        public DateTime? CancelledAt { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public virtual ICollection<TrackingEvent>? TrackingEvents { get; set; }
        [MaxLength(1000)]
        public string? CustomerNotes { get; set; }

        [MaxLength(1000)]
        public string? InternalNotes { get; set; }

        [MaxLength(500)]
        public string? CancellationReason { get; set; }

        public Guid? CancelledByUserId { get; set; }

        public Guid? PaymentId { get; set; }
        public void UpdateProgressStep()
        {
            ProgressStep = Status switch
            {
                OrderStatus.Pending => 0,
                OrderStatus.Confirmed => 1,
                OrderStatus.InProduction or OrderStatus.Printed => 2,
                OrderStatus.Shipped or OrderStatus.InTransit => 3,
                OrderStatus.Delivered => 4,
                _ => ProgressStep
            };
        }

        public int GetProgressPercentage()
        {
            return ProgressStep switch
            {
                0 => 0,
                1 => 25,
                2 => 50,
                3 => 75,
                4 => 100,
                _ => 0
            };
        }

        public string GetProgressText()
        {
            return Status switch
            {
                OrderStatus.Pending => "Order Pending",
                OrderStatus.Confirmed => "Order Confirmed",
                OrderStatus.InProduction => $"{GetProgressPercentage()}% Complete",
                OrderStatus.Printed => "Print Complete",
                OrderStatus.Shipped => $"{GetProgressPercentage()}% Complete",
                OrderStatus.InTransit => "In Transit",
                OrderStatus.Delivered => "Delivered",
                OrderStatus.Cancelled => "Cancelled",
                OrderStatus.Refunded => "Refunded",
                _ => "Unknown"
            };
        }

        public static string GenerateOrderNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"CQ-{timestamp}-{random}";
        }
    }
}
