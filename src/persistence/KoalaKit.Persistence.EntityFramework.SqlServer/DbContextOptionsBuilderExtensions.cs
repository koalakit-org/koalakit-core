using KoalaKit.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EntityFramework.SqlServer
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the context to use SqlServer.
        /// </summary>
        internal static DbContextOptionsBuilder UseSqlServer(this DbContextOptionsBuilder builder, string connectionString, Type? migrationsAssemblyMarker = default)
        {
            migrationsAssemblyMarker ??= typeof(SqlServerKoalaDbContextFactory);
            return builder.UseSqlServer(connectionString, db => db
                .MigrationsAssembly(migrationsAssemblyMarker.Assembly.GetName().Name));
        }
    }
}
