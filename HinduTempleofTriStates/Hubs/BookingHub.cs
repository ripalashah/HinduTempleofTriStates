using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Hubs
{
    public class BookingHub : Hub
    {
        // Method to send booking updates to connected clients
        public async Task SendBookingUpdate(string bookingId, string status)
        {
            await Clients.All.SendAsync("ReceiveBookingUpdate", $"Booking {bookingId}: {status}");
        }
    }
}
