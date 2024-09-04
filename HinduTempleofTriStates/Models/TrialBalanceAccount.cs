namespace HinduTempleofTriStates.Models
{
    public class TrialBalanceAccount
    {
        public Guid Id { get; set; }
        public required string AccountName { get; set; }
        public decimal DebitBalance { get; set; }
        public decimal CreditBalance { get; set; }
        public decimal DebitTotal { get; set; } // Add this property
        public decimal CreditTotal { get; set; } // Add this property
        public decimal NetBalance => DebitBalance - CreditBalance;
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
