using CRUDWithDifferentDB.PostgreSQL_Con;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string _PgSQLconnection=builder.Configuration.GetValue<string>("ConnectionStrings:PostgreSQLConnection")??string.Empty;

builder.Services.AddDbContext<PostgreSQLDbContext>(options => options.UseNpgsql(_PgSQLconnection), ServiceLifetime.Transient);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
