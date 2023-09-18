using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
