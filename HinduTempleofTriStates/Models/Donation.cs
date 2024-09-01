using System;
using System.ComponentModel.DataAnnotations;

namespace TempleManagementSystem.Models
{
    public class Donation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public required string DonorName { get; set; }

        [Required]
        public double Amount { get; set; }

        public required string DonationCategory { get; set; }

        [Required]
        public required string DonationType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public required string Phone { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        public required string Country { get; set; }
    }
}
