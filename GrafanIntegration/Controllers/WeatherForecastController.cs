using App.Metrics;
using App.Metrics.Counter;
using GrafanIntegration.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrafanIntegration.Controllers
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
        private readonly IMetrics _metrics;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMetrics metrics)
        {
            _logger = logger;
            _metrics = metrics;
        }

        [HttpGet]
        [Route("GetWeatherForecastGet")]
        public IEnumerable<WeatherForecast> Get()
        {
            _metrics.Measure.Counter.Increment(MetricsRegistry.CreatedWeatherforcastCounter);




            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet]
        [Route("GetWeatherForecastNext")]
        public IEnumerable<WeatherForecast> GetNext(bool Calls)
        {
            try
            {
                _metrics.Measure.Counter.Increment(MetricsRegistry.CreatedWeatherforcastCounterNext);

                if (Calls)
                {
                    
                     
                    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();
                }
                else
                {

                    throw new Exception("Test Exception 3");
                }
            }
            catch (Exception Ex) 
            {

                _metrics.Provider.Counter.Instance(new CounterOptions
                {
                    Name = "Error_Message",
                    MeasurementUnit = Unit.Custom(Ex.ToString()),
                }).Increment();

                throw;
            }
        }

        [HttpGet]
        [Route("GetWeatherForecastGet2")]
        public IEnumerable<WeatherForecast> Get2()
        {
            var errorCounter = _metrics.Manage.ToString();

            // Inside your error handling code
         
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}