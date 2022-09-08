using KoalaKit.Persistence.Specifications;

namespace Koalakit.Sample.SimpleApi.Entities.DbSpecs
{
    public class ForecastsSpecs : EntityISpec<Forecast>
    {
        public ForecastsSpecs ByExternalId(Guid? externalId)
        {
            if(externalId != null)
            {
                AddCriteria(a => a.ExternalId == externalId);
            }
            return this;
        }

        public ForecastsSpecs ByDate(DateTime? date)
        {
            if(date.HasValue)
            {
                AddCriteria(a => a.Date.Date == date.Value.Date);
            }
            return this;
        }
    }
}
