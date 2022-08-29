using KoalaKit.Caching;
using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Koalakit.Caching.Redis
{
    public class KoalaRedisCacheModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.AddKoalaCachingCore();
            koala.Services.AddScoped<KoalaRedisCache>();
            koala.Services.AddScoped(typeof(ICacheProvider<>), typeof(KoalaRedisCacheProvider<>));
            base.ConfigureKoala(koala);
        }
    }
}
