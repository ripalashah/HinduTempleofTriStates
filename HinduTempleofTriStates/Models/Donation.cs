using System;
using System.ComponentModel.DataAnnotations;

namespace TempleManagementSystem.Models
{
    public class Donation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string DonorName { get; set; }

        [Required]
        public double Amount { get; set; }

        public string DonationCategory { get; set; }

        [Required]
        public string DonationType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
