using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.MySql
{
    public class MySqlModule : EfCoreModuleBase
    {
        protected override string ProviderName => "MySql";

        protected override void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.ConfigureMySql(connectionString, migrationsAssemblyName);
    }
}
