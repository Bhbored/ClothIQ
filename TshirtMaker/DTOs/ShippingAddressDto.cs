using System.Text.Json.Serialization;
using TshirtMaker.Models.Core;
using TshirtMaker.Models.Orders;

namespace TshirtMaker.DTOs
{
    public class ShippingAddressDto : BaseEntityDto
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("address_line1")]
        public string AddressLine1 { get; set; } = string.Empty;

        [JsonPropertyName("address_line2")]
        public string? AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("state_province")]
        public string StateProvince { get; set; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; } = "US";

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; } = false;

        [JsonPropertyName("is_billing_address")]
        public bool IsBillingAddress { get; set; } = false;

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("is_validated")]
        public bool IsValidated { get; set; } = false;

        [JsonPropertyName("validated_at")]
        public DateTime? ValidatedAt { get; set; }

        [JsonPropertyName("validation_notes")]
        public string? ValidationNotes { get; set; }

        // Navigation property stored as JSON reference
        [JsonPropertyName("user_ref")]
        public Guid? User { get; set; }

        public ShippingAddress ToModel()
        {
            return new ShippingAddress
            {
                Id = this.Id,
                UserId = this.UserId,
                FullName = this.FullName,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2,
                City = this.City,
                StateProvince = this.StateProvince,
                PostalCode = this.PostalCode,
                CountryCode = this.CountryCode,
                Country = this.Country,
                IsDefault = this.IsDefault,
                IsBillingAddress = this.IsBillingAddress,
                Label = this.Label,
                IsValidated = this.IsValidated,
                ValidatedAt = this.ValidatedAt,
                ValidationNotes = this.ValidationNotes,
                User = this.User.HasValue ? new User { Id = this.User.Value } : null,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt
            };
        }

        public static ShippingAddressDto FromModel(ShippingAddress shippingAddress)
        {
            return new ShippingAddressDto
            {
                Id = shippingAddress.Id,
                UserId = shippingAddress.UserId,
                FullName = shippingAddress.FullName,
                PhoneNumber = shippingAddress.PhoneNumber,
                Email = shippingAddress.Email,
                AddressLine1 = shippingAddress.AddressLine1,
                AddressLine2 = shippingAddress.AddressLine2,
                City = shippingAddress.City,
                StateProvince = shippingAddress.StateProvince,
                PostalCode = shippingAddress.PostalCode,
                CountryCode = shippingAddress.CountryCode,
                Country = shippingAddress.Country,
                IsDefault = shippingAddress.IsDefault,
                IsBillingAddress = shippingAddress.IsBillingAddress,
                Label = shippingAddress.Label,
                IsValidated = shippingAddress.IsValidated,
                ValidatedAt = shippingAddress.ValidatedAt,
                ValidationNotes = shippingAddress.ValidationNotes,
                User = shippingAddress.User?.Id,
                CreatedAt = shippingAddress.CreatedAt,
                UpdatedAt = shippingAddress.UpdatedAt
            };
        }
    }
}