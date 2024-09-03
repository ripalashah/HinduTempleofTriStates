namespace HinduTempleofTriStates.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid LedgerAccountId { get; set; }
        public required LedgerAccount LedgerAccount { get; set; }
        public bool Reconciled { get; set; }
        public DateTime? ReconciliationDate { get; set; }
    }
}
