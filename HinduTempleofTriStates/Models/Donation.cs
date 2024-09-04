using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HinduTempleofTriStates.Models
{
    public class Donation
    {
        [Key]
        public Guid Id { get; set; } // Primary key

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
        public Guid AccountId { get; set; } // Foreign key

        [ForeignKey("AccountId")]
        public required Account Account { get; set; } // Navigation property

        // Foreign Key to LedgerAccount
        public Guid LedgerAccountId { get; set; } // Foreign key

        [ForeignKey("LedgerAccountId")]
        public LedgerAccount? LedgerAccount { get; set; } // Navigation property

        // Default constructor for EF Core and other scenarios
        public Donation() { }

        // Parameterized constructor for convenience
        public Donation(Guid id, string donorName, decimal amount, string donationCategory,
                        string donationType, DateTime date, string phone,
                        string city, string state, string country,
                        Guid accountId, LedgerAccount? ledgerAccount = null)
        {
            Id = id;
            DonorName = donorName;
            Amount = amount;
            DonationCategory = donationCategory;
            DonationType = donationType;
            Date = date;
            Phone = phone;
            City = city;
            State = state;
            Country = country;
            AccountId = accountId;
            LedgerAccount = ledgerAccount;
        }
    }
}
