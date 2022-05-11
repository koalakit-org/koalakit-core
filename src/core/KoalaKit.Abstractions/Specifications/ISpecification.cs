using System.Linq.Expressions;

namespace KoalaKit.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        Expression<Func<T, bool>> ToExpression();
    }
}