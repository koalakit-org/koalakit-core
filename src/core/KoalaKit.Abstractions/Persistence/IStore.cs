using KoalaKit.Persistence.Specifications;

namespace KoalaKit.Persistence
{
    public interface IStore<TEntity> where TEntity : class, IKoalaEntity
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> CountAsync(IEntityISpec<TEntity> specification, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(IEntityISpec<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(IEntityISpec<TEntity> specification, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> ListAsync(IEntityISpec<TEntity> specification, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEntityISpec<TEntity> specification, Func<TEntity?, ValueTask> update, CancellationToken cancellationToken = default);
    }
}