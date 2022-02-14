using System.Linq.Expressions;

namespace KoalaKit.Persistence.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool>? predicate;

        public bool IsSatisfiedBy(T entity)
        {
            predicate ??= ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
