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

        [HttpGet(Name = "get")]
        public async Task<ActionResult<IEnumerable<ForecastFetchResult>>> Get([FromQuery] Guid? id, DateTime? date)
        {
            var list = await store.ListAsync(new ForecastsSpecs().ByExternalId(id).ByDate(date));

            if (list == null)
            {
                return Ok(Enumerable.Empty<ForecastFetchResult>());
            }
            var result = list.Select(a => new ForecastFetchResult(a.Date, a.TemperatureC, a.TemperatureF));
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
