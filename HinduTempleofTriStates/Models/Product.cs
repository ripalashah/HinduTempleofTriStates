namespace HinduTempleofTriStates.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public int Quantity { get; set; }
        public Guid SupplierId { get; set; }

        public required Supplier Supplier { get; set; }
    }
}
