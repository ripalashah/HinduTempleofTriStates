namespace HinduTempleofTriStates.Models
{
    public class CashTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Income { get; set; } // Add this property
        public decimal Expense { get; set; } // Add this property
        public required string Type { get; set; } // "Income" or "Expense"
        public CashTransaction(DateTime date, string description, decimal income, decimal expense, string type)
        {
            Date = date;
            Description = description;
            Income = income;
            Expense = expense;
            Type = type;
        }
    }
}
