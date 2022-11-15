using System.Linq.Expressions;
using EFCore.BulkExtensions;
using KoalaKit.Persistence.Specifications;
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
                entity.CreatedUtc = DateTime.UtcNow;
                entity.ExternalId = Guid.NewGuid();
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

        public async Task<int> CountAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var count = await MapSpecification(dbContext.Set<TEntity>().AsQueryable(), specification).CountAsync();
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

        public async Task DeleteManyAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            await DoWork(async dbContext =>
            {
                var query = MapSpecification(dbContext.Set<TEntity>().AsQueryable(), specification);
                await query.BatchDeleteAsync(cancellationToken);
            }, cancellationToken);
        }

        public async Task<TEntity?> GetLastItem(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var query = MapIncludes(specification, dbContext.Set<TEntity>().AsQueryable());

                var result = await MapSpecification(query, specification).OrderByDescending(a => a.CreatedUtc).FirstOrDefaultAsync();
                return result;
            }, cancellationToken);

        }
        public async Task<TEntity?> FindAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var query = MapIncludes(specification, dbContext.Set<TEntity>().AsQueryable());

                var result = await MapSpecification(query, specification).FirstOrDefaultAsync();
                return result;
            }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ListAsync(IEntitySpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await DoWork(async dbContext =>
            {
                var query = MapIncludes(specification, dbContext.Set<TEntity>().AsQueryable());

                return await MapSpecification(query, specification).ToListAsync<TEntity>(cancellationToken);
            }, cancellationToken);
        }

        public async Task UpdateAsync(IEntitySpecification<TEntity> specification, Func<TEntity?, ValueTask> update, CancellationToken cancellationToken = default)
        {
            await DoWork(async dbContext =>
            {
                var query = MapIncludes(specification, dbContext.Set<TEntity>().AsQueryable());
                var entity = await MapSpecification(query, specification).FirstOrDefaultAsync();
                await update(entity);
                if (entity != null)
                    entity.UpdatedUtc = DateTime.UtcNow;

            }, cancellationToken);
        }

        protected IQueryable<TEntity> MapSpecification(IQueryable<TEntity> query, IEntitySpecification<TEntity> specification)
        {
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);
            return query;
        }
        protected IQueryable<TEntity> MapIncludes(IEntitySpecification<TEntity> specification, IQueryable<TEntity> query)
        {
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            return query;
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