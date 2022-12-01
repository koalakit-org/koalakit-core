using System.Text.Json;

namespace KoalaKit.Cosmetics
{
    // TODO: Implement tokenization
    public class TokenizationService : ITokenizationService
    {
        public TData? GetData<TData>(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return default(TData);
                }
                return JsonSerializer.Deserialize<TData>(token, new JsonSerializerOptions
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
            var token = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                IncludeFields = true,
            });
            return token;
        }
    }
}
