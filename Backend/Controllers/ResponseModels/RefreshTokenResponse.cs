using System.ComponentModel.DataAnnotations;

namespace EStore.Controllers.ResponseModels
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; } = null!;

        public string ValidTo { get; set; } = null!;
    }
}
