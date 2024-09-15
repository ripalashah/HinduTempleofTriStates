namespace HinduTempleofTriStates.Models
{
    public class StockReportViewModel
    {
        public required string ProductName { get; set; }
        public required string Category { get; set; }
        public int InitialQuantity { get; set; }
        public int StockInQuantity { get; set; }
        public int StockOutQuantity { get; set; }
        public int CurrentStock { get; set; }
    }
}
