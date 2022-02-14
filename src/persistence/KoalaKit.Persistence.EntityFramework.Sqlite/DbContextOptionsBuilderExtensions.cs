using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Sqlite
{
    internal static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseSqlite(this DbContextOptionsBuilder builder, string connectionString = "Data Source=elsa.sqlite.db;Cache=Shared;")
            => builder.UseSqlite(connectionString, db => db
                .MigrationsAssembly(typeof(SqliteKoalaDbContextFactory).Assembly.GetName().Name)
                .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
