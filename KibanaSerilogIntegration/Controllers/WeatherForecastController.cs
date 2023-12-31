using Microsoft.AspNetCore.Mvc;

namespace KibanaSerilogIntegration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("WeatherForecastReport")]
        public IActionResult WeatherForecastReport()
        {
            try
            {
                 Task.Delay(12342);

                throw new Exception("Logger Test Exception New Twst   afwtesrhgsdfhsfe. gghghh   jhgfdsasdfgh 1111111111111");     
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, message: "Something went wrong New!");

                throw;
                //return new StatusCodeResult(500);
            }

            _logger.LogInformation("Hello From action.");

            var Res = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            return Ok(Res);
        }


        [HttpGet]
        [Route("WeatherForecastReport2")]
        public IActionResult WeatherForecastReport2()
        {
            try
            {
                Task.Delay(12342);

                throw new Exception("Test Error for New Integration new with Alignment   check For the Best");
            }
            catch (Exception ex)
            {

                //_logger.LogError(ex, message: "Something went wrong New!");

                throw;
                //return new StatusCodeResult(500);
            }

        }
    }
}