using KoalaKit.Persistence.Specifications;

namespace KoalaKit.Persistence
{
    public interface IStore<TEntity> where TEntity : class, IKoalaEntity
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> CountAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> GetLastItem(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> ListAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEntitySpecification<TEntity> specification, Func<TEntity?, ValueTask> update, CancellationToken cancellationToken = default);
        Task UpdateManyAsync(IEntitySpecification<TEntity> specification, Func<TEntity?, ValueTask> update, CancellationToken cancellationToken = default);
    }
}