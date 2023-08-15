using AtoZASPNETCore.SignalR;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSignalR();

var app = builder.Build();
app.MapPost("broadcast",async (string message, IHubContext<ChatHub,IChatClient> context)=>
{
    await context.Clients.All.ReceiveMessage(message);

    return Results.NoContent();
});

app.UseHttpsRedirection();
app.MapHub<ChatHub>("chat-hub");
app.Run();
