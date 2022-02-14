using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore
{
    public class KoalaDbContext : DbContext
    {
        private const string DefaultSchema = "Koala";
        private const string DefaultMigrationsHistoryTable = "__EFMigrationsHistory";
        public KoalaDbContext(DbContextOptions options) : base(options) { }
        
        public static string Schema => DefaultSchema;
        public static string MigrationsHistoryTable => DefaultMigrationsHistoryTable;
    }
}
