using KoalaKit.Persistence.Specifications;

namespace KoalaKit.Persistence.EFCore
{
    internal class EfCoreSpecificationEvaluator<T> : SpecificationEvaluator<T>
        where T : IKoalaEntity
    {
        //public override IQueryable<T> GetQuery(IQueryable<T> query, IEntitySpecification<T> specification)
        //{
        //    var queryResult = base.GetQuery(query, specification);

        //    queryResult = specification.Includes.Aggregate(
        //        query,
        //        (current, include) => {
        //            if (include.ignoreFilter)
        //            {
        //                return current.Include(include.Item1).IgnoreQueryFilters();
        //            }

        //            return current.Include(include.Item1);
        //        });
        //}
    }
}
