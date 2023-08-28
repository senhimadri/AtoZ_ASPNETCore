using MinimalAPI.Data;
using MinimalAPI.Endpoints;
using MinimalAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddDbContext<SQLLightDBContext>();

var app = builder.Build();

app.MapCustomerEndpoints();


app.Run();
