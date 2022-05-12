using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Oracle
{
    public class OracleModule : EfCoreModuleBase
    {
        protected override string ProviderName => "Oracle";
        protected override void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.ConfigureOracle(connectionString, migrationsAssemblyName);
    }
}
