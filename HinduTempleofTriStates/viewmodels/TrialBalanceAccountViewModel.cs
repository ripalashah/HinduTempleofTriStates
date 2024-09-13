namespace viewmodels
{
    internal class TrialBalanceAccountViewModel
    {
        public required string AccountName { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public decimal NetBalance { get; set; }
    }
}