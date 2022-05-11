using KoalaKit.Specifications;
using System.Linq.Expressions;

namespace KoalaKit.Persistence.Specifications
{
    public abstract class EntitySpecification<TEntity> : ISpecification<TEntity>
        where TEntity : IKoalaEntity
    {
        private Expression<Func<TEntity, bool>> criteria = l =>true;
        private List<Expression<Func<TEntity, object>>> includes = new();
        public bool IsSatisfiedBy(TEntity candidate)
        {
            if (candidate == null) return true;
            var predicate = criteria.Compile();
            return predicate(candidate);
        }
        public Expression<Func<TEntity, bool>> Criteria => criteria;
        public List<Expression<Func<TEntity, object>>> Includes => includes;

        protected virtual void AddCriteria(Expression<Func<TEntity, bool>> newCriteria)
        {
            if (newCriteria == null) return;

            var invokedExpression = Expression.Invoke(newCriteria, criteria.Parameters.Cast<Expression>());
            var binary = Expression.AndAlso(criteria.Body, invokedExpression);
            criteria = Expression.Lambda<Func<TEntity, bool>>(binary, criteria.Parameters);
        }

        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
