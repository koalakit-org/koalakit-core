using KoalaKit.Persistence.Specifications;

namespace Koalakit.Sample.SimpleApi.Entities.DbSpecs
{
    public class ForecastsSpecs : EntitySpecification<Forecast>
    {
        public ForecastsSpecs ByExternalId(Guid externalId)
        {
            AddCriteria(a => a.ExternalId == externalId);
            return this;
        }

        public ForecastsSpecs ByDate(DateTime date)
        {
            AddCriteria(a => a.Date.Date == date);
            return this;
        }
    }
}
