namespace KoalaKit.Persistence.EFCore.DbFactoryServices
{
    public interface IContextFactory<out TDbContext>
    {
        TDbContext CreateDbContext();
    }
}
