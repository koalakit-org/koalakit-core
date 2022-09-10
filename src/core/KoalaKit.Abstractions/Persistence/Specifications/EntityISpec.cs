using System.Linq.Expressions;

namespace KoalaKit.Persistence.Specifications
{
    public abstract class EntityISpec<T> : IEntityISpec<T>
        where T : IKoalaEntity
    {
        private Expression<Func<T, bool>> criteria = a => true;
        private List<Expression<Func<T, object>>> includes = new();
        private List<string> includesStrings = new();

        public bool IsSatisfiedBy(T candidate)
        {
            if (criteria == null) return true;

            var predicate = criteria.Compile();
            return predicate(candidate);
        }

        public Expression<Func<T, bool>> Criteria => criteria ?? (a => true);
        public List<Expression<Func<T, object>>> Includes => includes;
        public List<string> IncludeStrings => includesStrings;

        protected virtual void AddCriteria(Expression<Func<T, bool>> newCriteria)
        {
            if (newCriteria == null) return;

            var invokedExpression = Expression.Invoke(newCriteria, criteria.Parameters.Cast<Expression>());
            var binary = Expression.AndAlso(criteria.Body, invokedExpression);
            criteria = Expression.Lambda<Func<T, bool>>(binary, criteria.Parameters);
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeExpression)
        {
            includesStrings.Add(includeExpression);
        }
    }
}
