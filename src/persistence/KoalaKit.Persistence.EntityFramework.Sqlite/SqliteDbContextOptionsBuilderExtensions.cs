using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Sqlite
{
    internal static class SqliteDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder ConfigureSqlite(this DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
            => builder.UseSqlite(connectionString, db => db
                .MigrationsAssembly(migrationsAssemblyName)
                .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
