using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.PostgreSql
{
    internal static class PostgreSqlDbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder ConfigurePostgreSql(this DbContextOptionsBuilder builder,
            string connectionString, string migrationsAssemblyName)
            => builder.UseNpgsql(connectionString, db => db
            .MigrationsAssembly(migrationsAssemblyName)
            .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
