using System.Security.Claims;

namespace Koala.Authentications.Jwt
{
    public interface IKoalaJwtGenerator
    {
        string GenerateAccessToken(params Claim[] claim);
        public string GenerateRefreshToken(string userId);
        public bool ValidateRefreshToken(string token, out string userId);
    }
}
