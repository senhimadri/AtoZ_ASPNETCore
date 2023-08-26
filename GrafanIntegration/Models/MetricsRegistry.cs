using App.Metrics;
using App.Metrics.Counter;

namespace GrafanIntegration.Models;

public  class MetricsRegistry
{
    public static CounterOptions CreatedWeatherforcastCounter => new CounterOptions() 
    {
        Name= "GetWeatherForecast",
        Context= "GetWeatherForecastAPI",
        MeasurementUnit=Unit.Calls
    };

    public static CounterOptions CreatedWeatherforcastCounterNext => new CounterOptions()
    {
        Name = "GetWeatherForecastNext",
        Context = "GetWeatherForecastAPINext",
        MeasurementUnit = Unit.Errors
    };
}
