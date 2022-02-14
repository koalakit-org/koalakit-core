using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore
{
    public class KoalaDbContext : DbContext
    {
        public KoalaDbContext(DbContextOptions options) : base(options) { }
    }
}
