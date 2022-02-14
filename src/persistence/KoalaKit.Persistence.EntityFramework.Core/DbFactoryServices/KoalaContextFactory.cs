using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.DbFactoryServices
{
    public class KoalaContextFactory<TKoalaContext> : IKoalaContextFactory where TKoalaContext : KoalaDbContext
    {
        private readonly IDbContextFactory<TKoalaContext> contextFactory;
        public KoalaContextFactory(IDbContextFactory<TKoalaContext> contextFactory) => this.contextFactory = contextFactory;
        public KoalaDbContext CreateDbContext() => contextFactory.CreateDbContext();
    }
}
