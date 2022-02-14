using KoalaKit.Persistence.EFCore.DbFactoryServices;
using KoalaKit.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Tasks
{
    public class CodFirstMigrationsTask : IKoalaTask
    {
        private readonly IKoalaContextFactory dbContextFactory;
        public CodFirstMigrationsTask(IKoalaContextFactory dbContextFactory) => this.dbContextFactory = dbContextFactory;

        public int Order => 0;
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = dbContextFactory.CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.DisposeAsync();
        }
    }
}
