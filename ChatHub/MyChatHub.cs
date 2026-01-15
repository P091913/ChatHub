using Microsoft.AspNetCore.SignalR;

namespace ChatHub;

public class MyChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        // essentially, this will allow us to receive message from users and broadcast them all to everyon
        // who is currently using the webpage  = mindblown right? no? poooooof
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}