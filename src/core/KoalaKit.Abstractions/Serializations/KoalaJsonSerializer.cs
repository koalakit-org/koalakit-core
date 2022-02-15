using System.Text;
using System.Text.Json;

namespace KoalaKit.Serializations
{
    public class KoalaJsonSerializer<TData> : ISerializer<TData>
    {
        public virtual TData? Deserialize(byte[] bytes)
            => JsonSerializer.Deserialize<TData>(Encoding.UTF8.GetString(bytes));

        public virtual byte[] Serialize(TData message)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
    }
}
