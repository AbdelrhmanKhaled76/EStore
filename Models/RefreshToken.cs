namespace EStore.Models
{

    public class RefreshToken
    {

        public DateTimeOffset ExpiryDate { get; set; }
        public string Value { get; set; } = null!;
        public virtual string LoginProvider { get; set; } = default!;

        public string Name { get; set; } = null!;

    }
}