using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class StockTransaction
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Description is required.")] 
        public required string Type { get; set; } // "Stock In" or "Stock Out"
        public required string Description { get; set; } // Additional field for better tracking

        public required Product Product { get; set; }
    }
}
