using Microsoft.AspNetCore.RateLimiting;
using MiddlewareExplanation;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalRateLimiterServices();


var app = builder.Build();

app.UseRateLimiter();

app.MapGet("/counter", () => "Hello World!");

app.Run();
