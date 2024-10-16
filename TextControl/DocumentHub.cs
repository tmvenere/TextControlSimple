using Microsoft.AspNetCore.SignalR;

public class DocumentHub : Hub
{
    public async Task JoinRoom(string roomName)
    {
        // Add the current connection to the specified group
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the group {roomName}.");
    }

    public async Task SendMessageToGroup(string roomName, string user, string message)
    {
        // Send a message to the specified group
        await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Remove from all groups upon disconnection
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "defaultRoom");
        await base.OnDisconnectedAsync(exception);
    }
}
