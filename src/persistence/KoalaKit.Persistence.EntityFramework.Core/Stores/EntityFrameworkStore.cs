using System.Linq.Expressions;
using EFCore.BulkExtensions;
using KoalaKit.Persistence.Specifications;
using KoalaKit.Specifications;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore
{
    public abstract class EntityFrameworkStore<TEntity, TContext> : IStore<TEntity>
    where TEntity : class, IKoalaEntity
    where TContext : KoalaDbContext
    {
        private readonly IDbContextFactory<TContext> dbContextFactory;

        protected EntityFrameworkStore(IDbContextFactory<TContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var result = await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
                return result.Entity;
            }, cancellationToken);
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DoWork(async dbContext =>
            {
                await dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            }, cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var count = await dbContext.Set<TEntity>().CountAsync(MapSpecification(specification), cancellationToken);
                return count;
            }, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await DoWork(async dbContext =>
            {
                var query = dbContext.Set<TEntity>().AsQueryable().Where(e => e.Id == id);
                await query.BatchDeleteAsync(cancellationToken);
            }, cancellationToken);
        }

        public async Task<int> DeleteManyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var query = dbContext.Set<TEntity>().AsQueryable()
                    .Where(MapSpecification(specification));
                return await query.BatchDeleteAsync(cancellationToken);
            }, cancellationToken);
        }

        public async Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var result = await dbContext.Set<TEntity>().FirstOrDefaultAsync(MapSpecification(specification));
                return result;
            }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(ISpecification<TEntity> specification, IOrderBy<TEntity>? orderBy = null, IPaging? paging = null, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {

                var dbSet = dbContext.Set<TEntity>();
                var queryable = dbSet.Where(MapSpecification(specification));

                if (orderBy != null)
                {
                    var orderByExpression = orderBy.OrderByExpression;
                    queryable = orderBy.SortDirection == SortDirection.Ascending ? queryable.OrderBy(orderByExpression) : queryable.OrderByDescending(orderByExpression);
                }

                if (paging != null)
                    queryable = queryable.Skip(paging.Skip).Take(paging.Take);

                var query = dbContext.Set<TEntity>().AsQueryable();
                return await query.Where(MapSpecification(specification)).ToListAsync(cancellationToken);
            }, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DoWork(async dbContext =>
            {
                var modelToUpdate = await dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == entity.Id || e.ExternalId == entity.ExternalId);
                if (modelToUpdate != null) modelToUpdate = entity;
            }, cancellationToken);
        }
        
        protected Expression<Func<TEntity, bool>> MapSpecification(ISpecification<TEntity> specification) => specification.ToExpression();

        protected async ValueTask DoWork(Func<TContext, ValueTask> work, CancellationToken cancellationToken)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            await work(dbContext);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        protected async ValueTask<TResult> DoWork<TResult>(Func<TContext, ValueTask<TResult>> work, CancellationToken cancellationToken)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var result = await work(dbContext);
            await dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}