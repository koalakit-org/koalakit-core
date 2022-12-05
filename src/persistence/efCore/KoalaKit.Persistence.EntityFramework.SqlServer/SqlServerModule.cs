using KoalaKit.Modules;
using KoalaKit.Options;
using KoalaKit.Persistence.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.SqlServer
{
    public class SqlServerModule : KoalaModuleBase
    {
        const string ProviderName = "SqlServer";

        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.UseEfPersistence(options =>
            {
                options.ConfigureSqlServer(koala.GetConnectionString(ProviderName), koala.GetMigrationAssemblyName());
            });
        }
    }
}
