using KoalaKit.Persistence.EFCore;

namespace KoalaKit.Persistence.EntityFramework.Memory
{
    public class KoalaMemoryDbModule : EfCoreModuleBase
    {
        protected override string ProviderName => "SqlServer";

        protected override void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.ConfigureSqlServer(connectionString, migrationsAssemblyName);
    }
}
