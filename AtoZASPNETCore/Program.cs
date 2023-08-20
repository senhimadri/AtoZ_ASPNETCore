using AtoZASPNETCore.SignalR;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSignalR();


#region Advanced Rate Limiting Use Cases In .NET
//Link: https://www.milanjovanovic.tech/blog/advanced-rate-limiting-use-cases-in-dotnet#rate-limiting-users-by-ip-address

//builder.Services.AddRateLimiter(options =>
//{
//    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

//    options.AddPolicy("fixed", httpContext =>
//        RateLimitPartition.GetFixedWindowLimiter(
//            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
//            factory: _ => new FixedWindowRateLimiterOptions
//            {
//                PermitLimit = 10,
//                Window = TimeSpan.FromSeconds(10),
//            }));
//});

//builder.Services.AddRateLimiter(rateLimiterOption =>
//{
//    rateLimiterOption.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

//    rateLimiterOption.AddTokenBucketLimiter("token", option =>
//    {
//        option.TokenLimit = 1000;
//        option.ReplenishmentPeriod = TimeSpan.FromHours(1);
//        option.TokensPerPeriod = 700;
//        option.AutoReplenishment = true;
//    });

//});

//builder.Services.AddRateLimiter(options =>
//{
//    options.AddPolicy("fixed-by-ip", httpContext =>
//        RateLimitPartition.GetFixedWindowLimiter(
//            httpContext.Request.Headers["X-Forwarded-For"].ToString(),
//            factory: _ => new FixedWindowRateLimiterOptions
//            {
//                PermitLimit = 10,
//                Window = TimeSpan.FromSeconds(10)

//            }));
//});

//builder.Services.AddRateLimiter(option =>
//{
//    option.AddPolicy("fixed-by-user", httpContext =>
//        RateLimitPartition.GetFixedWindowLimiter(
//            partitionKey: httpContext.User.Identities?.ToString(),
//            factory: _ => new FixedWindowRateLimiterOptions
//            {
//                PermitLimit = 10,
//                Window = TimeSpan.FromMinutes(1)
//            }));
//});
#endregion






var app = builder.Build();
app.MapPost("broadcast",async (string message, IHubContext<ChatHub,IChatClient> context)=>
{
    await context.Clients.All.ReceiveMessage(message);

    return Results.NoContent();
});

app.UseHttpsRedirection();

//app.UseRateLimiter();
app.MapHub<ChatHub>("chat-hub");
app.Run();
