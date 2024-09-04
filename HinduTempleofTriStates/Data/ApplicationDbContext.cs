using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Pages;

namespace HinduTempleofTriStates.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<Fund> Funds { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for financial fields
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LedgerAccount>()
                .Property(l => l.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            // Donations to LedgerAccount relationship
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.LedgerAccount)
                .WithMany(l => l.Donations)
                .HasForeignKey(d => d.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transactions to LedgerAccount relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.LedgerAccount)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // CashTransactions to LedgerAccount relationship
            modelBuilder.Entity<CashTransaction>()
                .HasOne(ct => ct.LedgerAccount)
                .WithMany(l => l.CashTransactions)
                .HasForeignKey(ct => ct.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // GeneralLedgerEntry configuration
            modelBuilder.Entity<GeneralLedgerEntry>()
            .HasKey(g => g.EntryId); // Define EntryId as the primary key

            modelBuilder.Entity<GeneralLedgerEntry>()
                .Property(g => g.EntryId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v))
                .HasColumnName("EntryId");


            // Optional: Add indexes for performance
            // modelBuilder.Entity<Account>().HasIndex(a => a.AccountName);
            // modelBuilder.Entity<Donation>().HasIndex(d => d.DonorName);
        }
    }
}
