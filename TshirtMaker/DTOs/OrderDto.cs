using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Enums;
using TshirtMaker.Models.Orders;

namespace TshirtMaker.DTOs
{
    public class OrderDto : BaseEntityDto
    {
        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [JsonPropertyName("progress_step")]
        public int ProgressStep { get; set; } = 1;

        // Navigation properties stored as JSON arrays
        [JsonPropertyName("items")]
        public List<Guid>? Items { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("shipping_cost")]
        public decimal ShippingCost { get; set; } = 0;

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; } = 0;

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; } = 0;

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "USD";

        [JsonPropertyName("shipping_address_id")]
        public Guid? ShippingAddressId { get; set; }

        // Navigation properties stored as JSON references
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        [JsonPropertyName("shipping_address_ref")]
        public Guid? ShippingAddress { get; set; }

        [JsonPropertyName("shipping_method")]
        public string? ShippingMethod { get; set; }

        [JsonPropertyName("carrier_name")]
        public string? CarrierName { get; set; }

        [JsonPropertyName("tracking_number")]
        public string? TrackingNumber { get; set; }

        [JsonPropertyName("placed_at")]
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("confirmed_at")]
        public DateTime? ConfirmedAt { get; set; }

        [JsonPropertyName("production_started_at")]
        public DateTime? ProductionStartedAt { get; set; }

        [JsonPropertyName("shipped_at")]
        public DateTime? ShippedAt { get; set; }

        [JsonPropertyName("delivered_at")]
        public DateTime? DeliveredAt { get; set; }

        [JsonPropertyName("cancelled_at")]
        public DateTime? CancelledAt { get; set; }

        [JsonPropertyName("estimated_delivery_date")]
        public DateTime? EstimatedDeliveryDate { get; set; }

        // Navigation properties stored as JSON arrays
        [JsonPropertyName("tracking_events")]
        public List<Guid>? TrackingEvents { get; set; }

        [JsonPropertyName("customer_notes")]
        public string? CustomerNotes { get; set; }

        [JsonPropertyName("internal_notes")]
        public string? InternalNotes { get; set; }

        [JsonPropertyName("cancellation_reason")]
        public string? CancellationReason { get; set; }

        [JsonPropertyName("cancelled_by_user_id")]
        public Guid? CancelledByUserId { get; set; }

        [JsonPropertyName("payment_id")]
        public Guid? PaymentId { get; set; }

        public Order ToModel()
        {
            return new Order
            {
                Id = this.Id,
                OrderNumber = this.OrderNumber,
                UserId = this.UserId,
                Status = this.Status,
                ProgressStep = this.ProgressStep,
                Subtotal = this.Subtotal,
                ShippingCost = this.ShippingCost,
                Tax = this.Tax,
                Discount = this.Discount,
                Total = this.Total,
                Currency = this.Currency,
                ShippingAddressId = this.ShippingAddressId,
                ShippingMethod = this.ShippingMethod,
                CarrierName = this.CarrierName,
                TrackingNumber = this.TrackingNumber,
                PlacedAt = this.PlacedAt,
                ConfirmedAt = this.ConfirmedAt,
                ProductionStartedAt = this.ProductionStartedAt,
                ShippedAt = this.ShippedAt,
                DeliveredAt = this.DeliveredAt,
                CancelledAt = this.CancelledAt,
                EstimatedDeliveryDate = this.EstimatedDeliveryDate,
                CustomerNotes = this.CustomerNotes,
                InternalNotes = this.InternalNotes,
                CancellationReason = this.CancellationReason,
                CancelledByUserId = this.CancelledByUserId,
                PaymentId = this.PaymentId,
                Items = this.Items?.Select(id => new OrderItem { Id = id }).ToList(),
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                ShippingAddress = this.ShippingAddress.HasValue ? new ShippingAddress { Id = this.ShippingAddress.Value } : null,
                TrackingEvents = this.TrackingEvents?.Select(id => new TrackingEvent { Id = id }).ToList(),
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static OrderDto FromModel(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                Status = order.Status,
                ProgressStep = order.ProgressStep,
                Subtotal = order.Subtotal,
                ShippingCost = order.ShippingCost,
                Tax = order.Tax,
                Discount = order.Discount,
                Total = order.Total,
                Currency = order.Currency,
                ShippingAddressId = order.ShippingAddressId,
                ShippingMethod = order.ShippingMethod,
                CarrierName = order.CarrierName,
                TrackingNumber = order.TrackingNumber,
                PlacedAt = order.PlacedAt,
                ConfirmedAt = order.ConfirmedAt,
                ProductionStartedAt = order.ProductionStartedAt,
                ShippedAt = order.ShippedAt,
                DeliveredAt = order.DeliveredAt,
                CancelledAt = order.CancelledAt,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                CustomerNotes = order.CustomerNotes,
                InternalNotes = order.InternalNotes,
                CancellationReason = order.CancellationReason,
                CancelledByUserId = order.CancelledByUserId,
                PaymentId = order.PaymentId,
                Items = order.Items?.Select(i => i.Id).ToList(),
                User = order.User?.Id,
                ShippingAddress = order.ShippingAddress?.Id,
                TrackingEvents = order.TrackingEvents?.Select(t => t.Id).ToList(),
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}