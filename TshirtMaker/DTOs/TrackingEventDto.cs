using System.Text.Json.Serialization;
using TshirtMaker.Models.Orders;

namespace TshirtMaker.DTOs
{
    public class TrackingEventDto : BaseEntityDto
    {
        [JsonPropertyName("order_id")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; } = false;

        [JsonPropertyName("carrier_name")]
        public string? CarrierName { get; set; }

        [JsonPropertyName("carrier_code")]
        public string? CarrierCode { get; set; }

        [JsonPropertyName("event_type")]
        public string? EventType { get; set; }

        [JsonPropertyName("additional_info")]
        public string? AdditionalInfo { get; set; }

        [JsonPropertyName("display_order")]
        public int DisplayOrder { get; set; }

        // Navigation property stored as JSON reference
        [JsonPropertyName("order_ref")]
        public Guid? Order { get; set; }

        public TrackingEvent ToModel()
        {
            return new TrackingEvent
            {
                Id = this.Id,
                OrderId = this.OrderId,
                Title = this.Title,
                Description = this.Description,
                Location = this.Location,
                EventDate = this.EventDate,
                IsActive = this.IsActive,
                CarrierName = this.CarrierName,
                CarrierCode = this.CarrierCode,
                EventType = this.EventType,
                AdditionalInfo = this.AdditionalInfo,
                DisplayOrder = this.DisplayOrder,
                Order = this.Order.HasValue ? new Order { Id = this.Order.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static TrackingEventDto FromModel(TrackingEvent trackingEvent)
        {
            return new TrackingEventDto
            {
                Id = trackingEvent.Id,
                OrderId = trackingEvent.OrderId,
                Title = trackingEvent.Title,
                Description = trackingEvent.Description,
                Location = trackingEvent.Location,
                EventDate = trackingEvent.EventDate,
                IsActive = trackingEvent.IsActive,
                CarrierName = trackingEvent.CarrierName,
                CarrierCode = trackingEvent.CarrierCode,
                EventType = trackingEvent.EventType,
                AdditionalInfo = trackingEvent.AdditionalInfo,
                DisplayOrder = trackingEvent.DisplayOrder,
                Order = trackingEvent.Order?.Id,
                CreatedAt = trackingEvent.CreatedAt,
                UpdatedAt = trackingEvent.UpdatedAt
            };
        }
    }
}