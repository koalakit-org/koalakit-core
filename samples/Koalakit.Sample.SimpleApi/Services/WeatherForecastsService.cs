using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Entities;
using Koalakit.Sample.SimpleApi.Entities.DbSpecs;
using KoalaKit.Persistence;

namespace Koalakit.Sample.SimpleApi.Services
{
    public class WeatherForecastsService
    {
        private readonly IStore<Forecast> store;

        public WeatherForecastsService(IStore<Forecast> store)
        {
            this.store = store;
        }

        public async Task<ForecastFetchResult?> Get(Guid? id)
        {
            var spec = new ForecastsSpecs();

            if (id.HasValue) spec.ByExternalId(id.Value);

            var forecast = await store.FindAsync(spec);
            if (forecast == null)
            {
                return null;
            }
            return new ForecastFetchResult(forecast.ExternalId, forecast.Date, forecast.TemperatureC, forecast.TemperatureF);
        }

        public async Task<IEnumerable<ForecastFetchResult>?> Get(DateTime? date)
        {
            var spec = new ForecastsSpecs();

            if (date.HasValue) spec.ByDate(date.Value);


            var list = await store.ListAsync(spec);

            if (list == null)
            {
                return null;
            }
            return list.Select(a => new ForecastFetchResult(a.ExternalId, a.Date, a.TemperatureC, a.TemperatureF));
        }


        public async Task Create(ForecastCreateParameters parameters)
         => await store.AddAsync(new Forecast
         {
             Date = parameters.Date,
             TemperatureC = parameters.Temp,
         });
    }
}