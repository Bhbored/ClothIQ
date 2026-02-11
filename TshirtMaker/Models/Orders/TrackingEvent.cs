using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Orders
{
    public class TrackingEvent : BaseEntity
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [Required]
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = false;

        [MaxLength(100)]
        public string? CarrierName { get; set; }

        [MaxLength(100)]
        public string? CarrierCode { get; set; }

        [MaxLength(50)]
        public string? EventType { get; set; }

        [MaxLength(1000)]
        public string? AdditionalInfo { get; set; }

        public int DisplayOrder { get; set; }
        public static TrackingEvent CreateOrderConfirmed(Guid orderId)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "Order Confirmed",
                Description = "Your order has been confirmed and is being prepared",
                EventDate = DateTime.UtcNow,
                EventType = "confirmed",
                DisplayOrder = 1
            };
        }

        public static TrackingEvent CreateInProduction(Guid orderId)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "In Production",
                Description = "Your design is being printed",
                EventDate = DateTime.UtcNow,
                EventType = "production",
                IsActive = true,
                DisplayOrder = 2
            };
        }

        public static TrackingEvent CreateShipped(Guid orderId, string? location = null, string? trackingNumber = null)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "Package Shipped",
                Description = trackingNumber != null ? $"Tracking: {trackingNumber}" : "Your package is on its way",
                Location = location,
                EventDate = DateTime.UtcNow,
                EventType = "shipped",
                IsActive = true,
                DisplayOrder = 3
            };
        }

        public static TrackingEvent CreateInTransit(Guid orderId, string location)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "In Transit",
                Description = "Package is moving through the carrier network",
                Location = location,
                EventDate = DateTime.UtcNow,
                EventType = "in_transit",
                IsActive = true,
                DisplayOrder = 4
            };
        }

        public static TrackingEvent CreateOutForDelivery(Guid orderId, string? location = null)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "Out for Delivery",
                Description = "Your package is out for delivery today",
                Location = location,
                EventDate = DateTime.UtcNow,
                EventType = "out_for_delivery",
                IsActive = true,
                DisplayOrder = 5
            };
        }

        public static TrackingEvent CreateDelivered(Guid orderId, string? location = null)
        {
            return new TrackingEvent
            {
                OrderId = orderId,
                Title = "Delivered",
                Description = "Package has been delivered",
                Location = location ?? "Your Address",
                EventDate = DateTime.UtcNow,
                EventType = "delivered",
                IsActive = true,
                DisplayOrder = 6
            };
        }
    }
}
