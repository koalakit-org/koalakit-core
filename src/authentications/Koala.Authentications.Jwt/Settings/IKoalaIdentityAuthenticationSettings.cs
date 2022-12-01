namespace Koala.Authentications.Jwt
{
    public interface IKoalaIdentityAuthenticationSettings
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int TtlMinutes { get; set; }
    }
}
