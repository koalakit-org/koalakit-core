namespace KoalaKit.Persistence.Specifications
{
    public abstract class SpecificationEvaluator<T> where T : IKoalaEntity
    {
        public virtual IQueryable<T> GetQuery(IQueryable<T> query, IEntitySpecification<T> specification)
        {
            var resultQuery = query.AsQueryable();

            if(specification != null)
            {
                query = query.Where(specification.Criteria);
            }

            //TODO: Implement ordering, paging

            return resultQuery;
        }
    }
}
