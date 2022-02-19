using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Sqlite
{
    public class SqliteModule : EfCoreModuleBase
    {
        protected override string ProviderName => "Sqlite";

        protected override void Configure(DbContextOptionsBuilder builder, string connectionString)
            => builder.UseSqlite(connectionString);
        protected override string GetDefaultConnectionString() => "Data Source=koala.sqlite.db;Cache=Shared;";
    }
}
