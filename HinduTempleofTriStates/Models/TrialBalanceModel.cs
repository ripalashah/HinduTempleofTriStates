using System.Collections.Generic;

namespace HinduTempleofTriStates.Models
{
    public class TrialBalanceModel
    {
        // List of TrialBalanceAccount objects
        public List<TrialBalanceAccount> TrialBalanceAccounts { get; set; }
        public string AccountName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }


        public decimal TotalDebits => (decimal)Debit;
        public decimal TotalCredits => (decimal)Credit;
        // Constructor to initialize the list
        public TrialBalanceModel()
        {
            TrialBalanceAccounts = new List<TrialBalanceAccount>();
        }
    }
}
