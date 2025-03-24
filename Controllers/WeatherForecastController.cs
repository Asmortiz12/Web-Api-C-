using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private static List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            if (!_weatherForecasts.Any())
            {
                _weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToList();
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[Route("Get/WeatherForecast")]
        //[Route("[action]")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogDebug("Retorna el pronóstico del tiempo");
            return _weatherForecasts;
        }

        [HttpPost(Name = "PostWeatherForecast")]
        //[Route("Post/WeatherForecast")]
        public IActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            _weatherForecasts.Add(weatherForecast);
            return Ok(weatherForecast);
        }

        [HttpDelete("{index}")]
        //[Route("Delete/WeatherForecast/{index}")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= _weatherForecasts.Count)
            {
                return NotFound($"No existe un pronóstico en la posición {index}. El rango válido es 0-{_weatherForecasts.Count - 1}");
            }

            _weatherForecasts.RemoveAt(index);
            return Ok($"Pronóstico en la posición {index} eliminado correctamente");
        }

    }
}