using Microsoft.AspNetCore.Mvc;

namespace Koalakit.Sample.SimpleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
