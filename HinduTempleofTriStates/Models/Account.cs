using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public AccountTypeEnum AccountType { get; set; }

        [Required]
        [StringLength(100)] // Limit the length of the account name
        public required string AccountName { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive value")] // Balance must be a positive value
        public decimal Balance { get; set; }

        public ICollection<Donation> Donations { get; set; } = new List<Donation>(); // Navigation property
                                                                                     // Navigation property to handle transactions related to this account
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime UpdatedDate { get; internal set; }
    }
}
