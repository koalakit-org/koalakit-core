using KoalaKit.Options;
using Microsoft.AspNetCore.Builder;

namespace KoalaKit.Modules
{
    public class KoalaModuleBase : IKoalaModule
    {
        public virtual void ConfigureApp(IApplicationBuilder app)
        {
        }

        public virtual void ConfigureKoala(KoalaOptionsBuilder koala)
        {
        }
    }
}
