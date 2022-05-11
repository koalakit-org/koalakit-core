using KoalaKit.Persistence.Specifications;
using KoalaKit.Specifications;

namespace KoalaKit.Persistence
{
    public interface IStore<TEntity> where TEntity : class, IKoalaEntity
    {
        //Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindManyAsync(ISpecification<TEntity> specification, IOrderBy<TEntity>? orderBy = default, IPaging? paging = default, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}