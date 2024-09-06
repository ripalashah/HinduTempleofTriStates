// TrialBalanceModel.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace HinduTempleofTriStates.Models
{
    public class TrialBalanceAccount
    {
        public Guid Id { get; set; } // Unique identifier for the account
        public required string AccountName { get; set; } // Name of the account

        // Balances for Debit and Credit
        public decimal DebitBalance { get; set; }
        public decimal CreditBalance { get; set; }

        // Total Debit and Credit for the account
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }

        // Net balance is Debit - Credit
        public decimal NetBalance => DebitBalance - CreditBalance;
    }

    public class TrialBalanceModel
    {
        // List of TrialBalanceAccount objects
        public List<TrialBalanceAccount> TrialBalanceAccounts { get; set; } = new List<TrialBalanceAccount>();
        public TrialBalanceModel()
        {
            TrialBalanceAccounts = new List<TrialBalanceAccount>(); // Initialize
        }
        // Calculated properties for the entire trial balance
        public decimal TotalDebits => TrialBalanceAccounts.Sum(a => a.DebitTotal);
        public decimal TotalCredits => TrialBalanceAccounts.Sum(a => a.CreditTotal);
        public decimal NetBalance => TotalDebits - TotalCredits;

        // Constructor (optional) can be added here if needed
    }
}
