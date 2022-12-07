using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace KoalaKit.Cosmetics
{
    public class TokenizationService : ITokenizationService
    {
        private readonly IDataProtector dataProtector;
        public TokenizationService(IDataProtector dataProtector)
        {
            this.dataProtector = dataProtector;
        }
        public TData? GetData<TData>(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return default(TData);
                }

                var plainToken = dataProtector.Unprotect(token);
                return JsonSerializer.Deserialize<TData>(plainToken, new JsonSerializerOptions
                {
                    IncludeFields = true,
                });
            }
            catch (Exception)
            {
                return default(TData);
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
