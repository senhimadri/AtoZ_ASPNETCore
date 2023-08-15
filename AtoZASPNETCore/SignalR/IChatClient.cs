namespace AtoZASPNETCore.SignalR;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
