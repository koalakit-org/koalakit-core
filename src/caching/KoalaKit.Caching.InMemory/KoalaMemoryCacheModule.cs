using KoalaKit.Modules;
using KoalaKit.Options;

namespace KoalaKit.Caching.InMemory
{
    public class KoalaMemoryCacheModule : KoalaModuleBase
    {

        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            CacheProviderStorage.AddProvider(new MemoryCacheProvider());
            base.ConfigureKoala(koala);
        }
    }
}
