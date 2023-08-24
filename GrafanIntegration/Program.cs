using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore.Tracking;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;


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

builder.Host.UseMetricsWebTracking().UseMetrics(options =>
{
    options.EndpointOptions = endpointoptong =>
    {
        endpointoptong.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointoptong.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        endpointoptong.EnvironmentInfoEndpointEnabled = false;
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
