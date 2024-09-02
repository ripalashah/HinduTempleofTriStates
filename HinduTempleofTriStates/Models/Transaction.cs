namespace HinduTempleofTriStates.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public TransactionType TransactionType { get; set; }

        public int LedgerAccountId { get; set; }
        public required LedgerAccount LedgerAccount { get; set; }
    }
}
