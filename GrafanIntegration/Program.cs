using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore.Tracking;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using System.Threading.RateLimiting;
using App.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<KestrelServerOptions>(options=>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddMetrics();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseMetricsWebTracking();

builder.Host.UseMetrics(options =>
{
    options.EndpointOptions = endpointoptong =>
    {
        endpointoptong.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointoptong.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        endpointoptong.EnvironmentInfoEndpointEnabled = true;
    };
});

builder.Host.UseMetricsWebTracking();




var app = builder.Build();



// Configure error handling middleware to increment the error metric



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMetricsEndpoint();

app.MapControllers();

app.Run();
