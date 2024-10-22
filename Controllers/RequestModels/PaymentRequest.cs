using EStore.Models;

namespace EStore.Controllers.RequestModels
{
    using EStore.Models.DTOs;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class PaymentRequest
    {
    
        [JsonPropertyName("items")]
        public List<OrderItemDTO> Items { get; set; }

        [JsonPropertyName("billing_data")]
        [Required]
        public BillingData BillingData { get; set; }
    
    }


    public class BillingData
    {

        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "This Field is required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "This Field is required")]
        [MaxLength(50, ErrorMessage = "cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [JsonPropertyName("street")]
        [Required(ErrorMessage = "This Field is required")]
        public string Street { get; set; }

        [JsonPropertyName("phone_number")]
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Phone number must be a valid Egyptian number with 11 digits.")]
        public string PhoneNumber { get; set; }


        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }


    }


}
