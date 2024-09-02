namespace HinduTempleofTriStates.Models
{
    public class LedgerAccount
    {
        public int Id { get; set; }
        public required string AccountName { get; set; }
        public required string AccountType { get; set; } // Asset, Liability, Equity, Revenue, Expense
        public decimal Balance { get; set; }

        public required ICollection<Transaction> Transactions { get; set; }
    }
}
