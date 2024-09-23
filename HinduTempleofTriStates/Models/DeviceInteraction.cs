using System;

namespace HinduTempleofTriStates.Models
{
    public class DeviceInteraction
    {
        public Guid Id { get; set; }
        public Guid DonationId { get; set; } // Link to Donation if necessary
        public string DeviceName { get; set; } = string.Empty;
        public DateTime InteractionDate { get; set; } = DateTime.UtcNow;
        public string Action { get; set; } = string.Empty; // Example actions: CreateDonation, UpdateDonation, etc.
        public string Status { get; set; } = "Pending"; // Could be Pending, Completed, Failed, etc.
        public string ResponseMessage { get; set; } = string.Empty; // Holds any feedback or errors from the device
    }
}
