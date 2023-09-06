using AuthenticationAuthorization.JWTAuthentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJWTTokenServices(builder.Configuration);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

