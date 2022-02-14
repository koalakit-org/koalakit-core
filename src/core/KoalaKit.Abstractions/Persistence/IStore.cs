using KoalaKit.Persistence.Specifications;

namespace KoalaKit.Persistence
{
    public interface IStore<TEntity> where TEntity : class, IKoalaEntity
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TResult?> FindAsync<TResult>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task UpdateAsync(ISpecification<TEntity> specification, TEntity entity, CancellationToken cancellationToken = default);
    }
}
