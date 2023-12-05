namespace WebApiRestful.Domain.Entities.Common
{
    public class TokenBear
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SigningKey { get; set; }
    }
}
