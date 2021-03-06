using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.SqlServer
{
    public class SqlServerModule : EfCoreModuleBase
    {
        protected override string ProviderName => "SqlServer";

        protected override void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.ConfigureSqlServer(connectionString, migrationsAssemblyName);
    }
}
