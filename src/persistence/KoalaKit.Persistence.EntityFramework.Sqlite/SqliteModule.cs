using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Sqlite
{
    public class SqliteModule : EfCoreModuleBase
    {
        protected override string ProviderName => "Sqlite";
        protected override void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.ConfigureSqlite(connectionString, migrationsAssemblyName);

        protected override string GetDefaultConnectionString() => "Data Source=koala.sqlite.db;Cache=Shared;";
    }
}
