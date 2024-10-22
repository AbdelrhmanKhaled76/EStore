using System.ComponentModel.DataAnnotations;

namespace EStore.Controllers.RequestModels
{
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; } = null!;

    }
}
