using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Entities;
using Koalakit.Sample.SimpleApi.Entities.DbSpecs;
using KoalaKit.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Koalakit.Sample.SimpleApi.Controllers
{
    [ApiController]
    [Route("weather/forecasts")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;
        private readonly IStore<Forecast> store;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStore<Forecast> store)
        {
            this.logger = logger;
            this.store = store;
        }

        [HttpGet]
        public async Task<ActionResult<ForecastFetchResult>> Get([FromQuery] Guid? id)
        {
            var spec = new ForecastsSpecs();

            if (id.HasValue) spec.ByExternalId(id.Value);

            var forecast = await store.FindAsync(spec);
            if (forecast == null)
            {
                return NotFound();
            }
            return Ok(new ForecastFetchResult(forecast.ExternalId, forecast.Date, forecast.TemperatureC, forecast.TemperatureF));
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ForecastFetchResult>>> Get([FromQuery] Guid? id, DateTime? date)
        {
            var spec = new ForecastsSpecs();

            if (date.HasValue) spec.ByDate(date.Value);

            if (id.HasValue) spec.ByExternalId(id.Value);

            var list = await store.ListAsync(spec);

            if (list == null)
            {
                return Ok(Enumerable.Empty<ForecastFetchResult>());
            }
            var result = list.Select(a => new ForecastFetchResult(a.ExternalId, a.Date, a.TemperatureC, a.TemperatureF));
            return Ok(result);
        }

        [HttpPost(Name = "create")]
        public async Task<IActionResult> Post([FromBody] ForecastCreateParameters parameters)
        {
            await store.AddAsync(new Forecast
            {
                Date = parameters.Date,
                TemperatureC = parameters.Temp,
            });
            return Ok();
        }
    }
}
