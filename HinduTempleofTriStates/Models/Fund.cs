using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models
{
    public class Fund
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; set; }  // Link to the account

        public required string AccountName { get; set; }
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Fund Name is required")]
        [StringLength(100, ErrorMessage = "Fund Name can't be longer than 100 characters")]
        public string FundName { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(250, ErrorMessage = "Description can't be longer than 250 characters")]
        public string Description { get; set; } = null!;
    }
}
