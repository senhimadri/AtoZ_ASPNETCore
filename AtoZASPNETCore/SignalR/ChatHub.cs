using Microsoft.AspNetCore.SignalR;

namespace AtoZASPNETCore.SignalR;

public sealed class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        //await Clients.All.SendAsync("ReceiveMessage",$"{Context.ConnectionId} has joined");
        await Clients.All.ReceiveMessage( $"{Context.ConnectionId} has joined");
    }

    public async Task SendMessage(string message)
    {
        //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} : {message}");
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} : {message}");
    }

}
