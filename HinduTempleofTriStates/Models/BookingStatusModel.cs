using System;

namespace HinduTempleofTriStates.Models
{
    public class BookingStatusModel
    {
        public Guid BookingId { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }

        // Additional properties as needed
    }
}
