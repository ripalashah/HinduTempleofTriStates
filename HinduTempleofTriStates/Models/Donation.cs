using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HinduTempleofTriStates.Models
{
    public class Donation
    {
        [Key]
        public Guid Id { get; set; } // Changed to Guid

        [Required]
        public required string DonorName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public required string DonationCategory { get; set; }

        [Required]
        public required string DonationType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string State { get; set; }

        [Required]
        public required string Country { get; set; }

        // Foreign Key to Account
        public int AccountId { get; set; } // Changed to Guid

        [ForeignKey("AccountId")]
        public required Account Account { get; set; } // Navigation property

        // Foreign Key to LedgerAccount
        public Guid LedgerAccountId { get; set; } // Changed to Guid

        [ForeignKey("LedgerAccountId")]
        public LedgerAccount? LedgerAccount { get; set; } // Navigation property
    }
}
