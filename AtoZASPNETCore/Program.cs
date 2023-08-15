using AtoZASPNETCore.SignalR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSignalR();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapHub<ChatHub>("chat-hub");



app.Run();
