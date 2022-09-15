using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Koalakit.Sample.SimpleApi.Controllers
{
    [ApiController]
    [Route("weather/forecasts")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastsService forecastsService;

        public WeatherForecastController(WeatherForecastsService forecastsService)
        {
            this.forecastsService = forecastsService;
        }

        [HttpGet]
        public async Task<ActionResult<ForecastFetchResult>> Get([FromQuery] Guid? id)
        {
            var forecast = await forecastsService.Get(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return Ok(forecast);
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ForecastFetchResult>>> Get([FromQuery] DateTime? date)
        {
            var list = await forecastsService.Get( date);

            if (list == null)
            {
                return Ok(Enumerable.Empty<ForecastFetchResult>());
            }
            return Ok(list);
        }

        [HttpPost(Name = "create")]
        public async Task<IActionResult> Post([FromBody] ForecastCreateParameters parameters)
        {
            await forecastsService.Create(parameters);
            return Ok();
        }
    }
}
