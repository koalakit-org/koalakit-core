using System.Linq.Expressions;

namespace KoalaKit.Persistence.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}
