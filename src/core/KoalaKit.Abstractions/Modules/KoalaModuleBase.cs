using KoalaKit.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace KoalaKit.Modules
{
    public class KoalaModuleBase : IKoalaModule
    {
        public virtual void ConfigureApp(IApplicationBuilder app)
        {
        }

        public virtual void ConfigureKoala(KoalaOptionsBuilder koala, IConfiguration configuration)
        {
        }
    }
}
