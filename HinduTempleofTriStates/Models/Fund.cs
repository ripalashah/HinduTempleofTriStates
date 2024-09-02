namespace HinduTempleofTriStates.Models
{
    public class Fund
    {
        public int Id { get; set; }
        public required string FundName { get; set; }
        public decimal Amount { get; set; }
        public required string Description { get; set; }
    }
}
