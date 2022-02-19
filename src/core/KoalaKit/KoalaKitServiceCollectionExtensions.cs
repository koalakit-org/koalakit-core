using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit
{
    public static class KoalaKitServiceCollectionExtensions
    {
        public static IServiceCollection AddKoalaKit(
            this IServiceCollection services,
            Action<KoalaOptionsBuilder> builder)
        => services.AddKoalaKitCore(builder);
    }
}
