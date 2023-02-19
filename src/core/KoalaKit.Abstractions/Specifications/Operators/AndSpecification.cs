using System.Linq.Expressions;

namespace KoalaKit.Specifications.Operators;

public class AndSpecification<T> : CompositeSpecification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }

    public override Expression<Func<T, bool>> ToExpression()
        => Left.ToExpression().And(Right.ToExpression());
}
