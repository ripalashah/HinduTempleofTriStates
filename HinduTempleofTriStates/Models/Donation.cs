using System.ComponentModel.DataAnnotations;

namespace HinduTempleofTriStates.Models

{
    public class Donation
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public string DonationCategory { get; set; } = string.Empty;

        [Required]
        public string DonationType { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        public Guid LedgerAccountId { get; set; }

        public virtual LedgerAccount? LedgerAccount { get; set; }
    }
}
