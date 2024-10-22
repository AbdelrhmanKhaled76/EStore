using System.ComponentModel.DataAnnotations;

namespace EStore.Controllers.RequestModels
{
    public class EmailRequest
    {
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "The email address is not valid.")]
        public string? To { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Body { get; set; }

        [Required]
        public string? RecipientName { get; set; }
    }
}
