namespace HinduTempleofTriStates.Models
{
    public class TrialBalanceAccount
    {
        public Guid Id { get; set; }
        public required string AccountName { get; set; }

        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public decimal NetBalance { get; set; }
    }

    public class TrialBalanceModel
    {
        public List<TrialBalanceAccount> TrialBalanceAccounts { get; set; } = new List<TrialBalanceAccount>();
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal NetBalance { get; set; }
    }
}
