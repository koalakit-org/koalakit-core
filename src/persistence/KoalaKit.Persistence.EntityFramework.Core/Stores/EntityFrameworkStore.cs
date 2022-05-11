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
                var count = await dbContext.Set<TEntity>().CountAsync(specification.Criteria, cancellationToken);
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
                    .Where(specification.Criteria);
                return await query.BatchDeleteAsync(cancellationToken);
            }, cancellationToken);
        }

        public async Task<TEntity?> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var query = specification.Includes.Aggregate(dbContext.Set<TEntity>().AsQueryable(), (current, include) => current.Include(include));

                var result = await query.FirstOrDefaultAsync(specification.Criteria);
                return result;
            }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(ISpecification<TEntity> specification, IOrderBy<TEntity>? orderBy = null, IPaging? paging = null, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {

                var query = specification.Includes.Aggregate(dbContext.Set<TEntity>().AsQueryable(), (current, include) => current.Include(include));

                var queryable = query.Where(specification.Criteria);

                if (orderBy != null)
                {
                    var orderByExpression = orderBy.OrderByExpression;
                    queryable = orderBy.SortDirection == SortDirection.Ascending ? queryable.OrderBy(orderByExpression) : queryable.OrderByDescending(orderByExpression);
                }

                if (paging != null)
                    queryable = queryable.Skip(paging.Skip).Take(paging.Take);

                return await query.Where(specification.Criteria).ToListAsync(cancellationToken);
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