namespace Koala.Authentications.Jwt
{
    public class KoalaIdentityAuthenticationSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TtlMinutes { get; set; } = 60;
    }
}
