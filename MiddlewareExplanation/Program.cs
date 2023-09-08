using MiddlewareExplanation.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalRateLimiterServices();
builder.Services.AddDefaultCorsPolicyServices();

var app = builder.Build();

//-------------Use Rate Limiter------------------------

app.UseRateLimiter();

//------------------***--------------------------------


//------------- Use Cors Policy ------------------------

app.UseCors();

app.UseCors("_speficCorsPolicy");

app.UseCors(options=> options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//------------------ *** ------------------------------




app.MapGet("/counter", () => "Hello World!");

app.Run();
