using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TshirtMaker.Models.Core;

namespace TshirtMaker.Models.Orders
{
    public class ShippingAddress : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string AddressLine1 { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string StateProvince { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(2)]
        public string CountryCode { get; set; } = "US";

        [MaxLength(100)]
        public string? Country { get; set; }

        public bool IsDefault { get; set; } = false;

        public bool IsBillingAddress { get; set; } = false;

        [MaxLength(50)]
        public string? Label { get; set; }

        public bool IsValidated { get; set; } = false;

        public DateTime? ValidatedAt { get; set; }

        [MaxLength(500)]
        public string? ValidationNotes { get; set; }
        public string GetFormattedAddress()
        {
            var parts = new List<string> { AddressLine1 };
            
            if (!string.IsNullOrEmpty(AddressLine2))
                parts.Add(AddressLine2);
            
            parts.Add($"{City}, {StateProvince} {PostalCode}");
            
            if (!string.IsNullOrEmpty(Country))
                parts.Add(Country);
            
            return string.Join("\n", parts);
        }

        public string GetSingleLineAddress()
        {
            var parts = new List<string> { AddressLine1 };
            
            if (!string.IsNullOrEmpty(AddressLine2))
                parts.Add(AddressLine2);
            
            parts.Add($"{City}, {StateProvince} {PostalCode}");
            
            return string.Join(", ", parts);
        }

        public bool IsInternational()
        {
            return CountryCode?.ToUpper() != "US";
        }
    }
}
