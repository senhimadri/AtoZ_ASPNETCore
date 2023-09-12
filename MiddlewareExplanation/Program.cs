using MiddlewareExplanation.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalRateLimiterServices();
builder.Services.AddSpecificCorsPolicyServices();

var app = builder.Build();

//-------------Use Rate Limiter------------------------

app.UseRateLimiter();

//------------------***--------------------------------


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//------------- Use Cors Policy ------------------------

app.UseCors();

//app.UseCors("_speficCorsPolicy");

//app.UseCors(options=> options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//------------------ *** ------------------------------

app.MapGet("/corscheck", () => "Hello World!").RequireCors("_speficCorsPolicy");

app.Run();
