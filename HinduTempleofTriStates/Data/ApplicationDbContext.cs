using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;

namespace HinduTempleofTriStates.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Existing DbSet for Donations
        public DbSet<Donation> Donations { get; set; }

        // Add DbSet for LedgerAccounts
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }

        // Add DbSet for Transactions
        public DbSet<Transaction> Transactions { get; set; }

        // Add DbSet for Funds
        public DbSet<Fund> Funds { get; set; }
        public DbSet<TrialBalanceAccount> TrialBalanceAccounts { get; set; } = default!;
        public DbSet<ProfitLossItem> ProfitLossItems { get; set; } = default!;
        public DbSet<GeneralLedgerEntry> GeneralLedgerEntries { get; set; } = default!;
        public DbSet<CashTransaction> CashTransactions { get; set; } = default!;

        // Optionally override OnModelCreating to customize model creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure LedgerAccount-Transaction relationship
            modelBuilder.Entity<LedgerAccount>()
                .HasMany(l => l.Transactions)
                .WithOne(t => t.LedgerAccount)
                .HasForeignKey(t => t.LedgerAccountId);

            // Other custom configurations (if needed)
        }
    }
}
