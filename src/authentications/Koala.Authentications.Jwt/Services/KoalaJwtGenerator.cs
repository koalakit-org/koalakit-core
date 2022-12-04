using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Koala.Authentications.Jwt
{
    public class KoalaJwtGenerator : IKoalaJwtGenerator
    {
        private readonly IKoalaIdentityAuthenticationSettings options;
        private readonly ConcurrentDictionary<string, string> tokens = new();

        public KoalaJwtGenerator(IKoalaIdentityAuthenticationSettings options)
        {
            this.options = options;
        }
        public string GenerateAccessToken(params Claim[] claims)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(options.Issuer, options.Audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(options.TtlMinutes), signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(string userId)
        {
            var token = Guid.NewGuid().ToString("N");
            tokens.TryAdd(token, userId);
            return token;
        }

        public bool ValidateRefreshToken(string token, out string userId)
        {
            userId = "";
            if (!tokens.TryRemove(token, out userId)) return false;

            return !string.IsNullOrEmpty(userId);
        }
    }
}
