using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }

        [Required]
        [StringLength(100)] // Optional: Limit the length of the account name
        public required string AccountName { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive value")] // Optional: Add range validation
        public decimal Balance { get; set; }

        public ICollection<Donation> Donations { get; set; } = new List<Donation>(); // Navigation property

        // Optional: Constructor to initialize the collection
        public Account()
        {
            Donations = new List<Donation>();
        }
    }
}
