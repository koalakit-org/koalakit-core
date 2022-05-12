using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace KoalaKit.Persistence.EFCore.MySql
{
    internal static class MySqlDbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder ConfigureMySql(this DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                db => db
                    .MigrationsAssembly(migrationsAssemblyName)
                    .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema)
                    .SchemaBehavior(MySqlSchemaBehavior.Ignore));
    }
}
