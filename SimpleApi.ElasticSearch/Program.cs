using SimpleApi.ElasticSearch.Configurations;
using SimpleApi.ElasticSearch.Models;
using SimpleApi.ElasticSearch.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));

builder.Services.AddSingleton<IElasticService, ElasticService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/create-index/{indexName}", async (IElasticService elasticService, string indexName) =>
{
    await elasticService.CreateIndexIfNotExistAsync(indexName);
    return Results.Ok($"Index '{indexName}' checked or created.");
});

app.MapPost("/api/user", async (IElasticService elasticService, User user) =>
{
    var result = await elasticService.AddOrUpdate(user);
    return result ? Results.Ok("User added/updated.") : Results.BadRequest("Failed to add/update user.");
});

app.MapPost("/api/users", async (IElasticService elasticService, IEnumerable<User> users, string indexName) =>
{
    var result = await elasticService.AddOrUpdateBulk(users, indexName);
    return result ? Results.Ok("Users added/updated.") : Results.BadRequest("Failed to add/update users.");
});

app.MapGet("/api/user/{key}", async (IElasticService elasticService, string key) =>
{
    var user = await elasticService.Get(key);
    return user != null ? Results.Ok(user) : Results.NotFound("User not found.");
});

app.MapGet("/api/users", async (IElasticService elasticService) =>
{
    var users = await elasticService.GetAll();
    return users != null ? Results.Ok(users) : Results.NotFound("No users found.");
});

app.MapDelete("/api/user/{key}", async (IElasticService elasticService, string key) =>
{
    var result = await elasticService.Remove(key);
    return result ? Results.Ok("User removed.") : Results.BadRequest("Failed to remove user.");
});

app.MapDelete("/api/users", async (IElasticService elasticService) =>
{
    var result = await elasticService.RemoveAll();
    return result ? Results.Ok("All users removed.") : Results.BadRequest("Failed to remove all users.");
});

app.Run();
