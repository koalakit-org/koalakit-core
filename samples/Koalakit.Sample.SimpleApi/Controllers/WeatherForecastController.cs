using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Services;
using KoalaKit.Messaging.Queuing;
using Microsoft.AspNetCore.Mvc;

namespace Koalakit.Sample.SimpleApi.Controllers
{
    [ApiController]
    [Route("weather/forecasts")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastsService forecastsService;
        private IMessageQueuingPublisher<WeatherForecastsPublishingParameters> publisher;

        public WeatherForecastController(WeatherForecastsService forecastsService, IMessageQueuingPublisher<WeatherForecastsPublishingParameters> publisher)
        {
            this.forecastsService = forecastsService;
            this.publisher = publisher;
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
        public IActionResult Post([FromBody] ForecastCreateParameters parameters)
        {
            publisher.Publish(new WeatherForecastsPublishingParameters
            {
                Date = parameters.Date,
                Temp = parameters.Temp,
            });
            return Ok();
        }
    }
}
