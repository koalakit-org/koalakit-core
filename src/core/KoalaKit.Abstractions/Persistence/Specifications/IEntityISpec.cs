using System.Linq.Expressions;
using KoalaKit.Specifications;

namespace KoalaKit.Persistence.Specifications
{
    public interface IEntityISpec<T> : ISpecification<T> where T : IKoalaEntity
    {
        List<Expression<Func<T, object>>> Includes { get; }
        public List<string> IncludeStrings { get; }
    }
}
