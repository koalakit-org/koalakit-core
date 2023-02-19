using System.Linq.Expressions;

namespace KoalaKit.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public bool IsSatisfiedBy(T candidate)
    {
        return ToExpression().Compile()(candidate);
    }

    public abstract Expression<Func<T, bool>> ToExpression();


    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.ToExpression();
    }
}
