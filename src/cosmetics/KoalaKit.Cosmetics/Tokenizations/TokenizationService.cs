using Microsoft.AspNetCore.DataProtection;
using System.Text.Json;

namespace KoalaKit.Cosmetics
{
    public class TokenizationService : ITokenizationService
    {
        private const string purpose = "ProtectData";
        private readonly IDataProtector dataProtector;
        public TokenizationService(IDataProtectionProvider dataProtector)
        {
            this.dataProtector = dataProtector.CreateProtector(purpose: purpose);
        }
        public TData? GetData<TData>(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return default;
                }

                var plainToken = dataProtector.Unprotect(token);
                return JsonSerializer.Deserialize<TData>(plainToken, new JsonSerializerOptions
                {
                    IncludeFields = true,
                });
            }
            catch (Exception)
            {
                return default;
            }
        }

        public string Tokenize<TData>(TData data)
        {
            var plainToken = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                IncludeFields = true,
            });

            var token = dataProtector.Protect(plainToken);
            return token;
        }
    }
}
