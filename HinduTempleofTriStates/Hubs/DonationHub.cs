using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Hubs
{
    public class DonationHub : Hub
    {
        // Method to send updates to connected clients when a new donation is received
        public async Task SendDonationUpdate(string donationId, string status)
        {
            // Sends a message to all connected clients about the status of a donation
            await Clients.All.SendAsync("ReceiveDonationUpdate", donationId, status);
        }

        // Method to send specific messages or updates to a particular group (if needed)
        public async Task SendGroupUpdate(string groupName, string message)
        {
            // Sends a message to a specific group of clients
            await Clients.Group(groupName).SendAsync("ReceiveGroupUpdate", message);
        }

        // Method to add a client to a specific group (useful for targeted updates)
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        // Method to remove a client from a specific group
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        // Override methods for connecting and disconnecting clients, if needed for logging or management
        public override async Task OnConnectedAsync()
        {
            // Handle logic when a client connects
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("ReceiveMessage", "Welcome to the Donation Hub!");
        }

        public override async Task OnDisconnectedAsync(System.Exception? exception)
        {
            // Handle logic when a client disconnects
            await base.OnDisconnectedAsync(exception);
            if (exception != null)
            {
                // Log or handle the disconnection error if necessary
            }
        }
    }
}
