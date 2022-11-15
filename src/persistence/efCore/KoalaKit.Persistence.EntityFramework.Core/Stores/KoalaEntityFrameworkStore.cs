using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Stores
{
    public class KoalaEntityFrameworkStore<TEntity> : EntityFrameworkStore<TEntity, KoalaDbContext>
        where TEntity : class, IKoalaEntity
    {
        public KoalaEntityFrameworkStore(IDbContextFactory<KoalaDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
