using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
                .ToTable("Accounts")
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LedgerAccount>()
                .Property(l => l.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CashTransaction>()
                .Property(ct => ct.Amount)
                .HasColumnType("decimal(18,2)");

            // Ignore properties that are not mapped to the database
            modelBuilder.Entity<CashTransaction>()
                .Ignore(ct => ct.Expense)
                .Ignore(ct => ct.Income);

            modelBuilder.Entity<Fund>()
                .Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fund>()
                .Property(f => f.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<GeneralLedgerEntry>()
                .Ignore(g => g.Balance);

            modelBuilder.Entity<GeneralLedgerEntry>()
                .Property(g => g.Credit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<GeneralLedgerEntry>()
                .Property(g => g.Debit)
                .HasColumnType("decimal(18,2)");

            // Configure relationships
            modelBuilder.Entity<GeneralLedgerEntry>()
                .HasOne(gl => gl.LedgerAccount)
                .WithMany(l => l.GeneralLedgerEntries)
                .HasForeignKey(gl => gl.LedgerAccountId);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.LedgerAccount)
                .WithMany(l => l.Donations)
                .HasForeignKey(d => d.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.LedgerAccount)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CashTransaction>()
                .HasOne(ct => ct.LedgerAccount)
                .WithMany(l => l.CashTransactions)
                .HasForeignKey(ct => ct.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure GeneralLedgerEntry relationships
            modelBuilder.Entity<GeneralLedgerEntry>()
                .HasKey(g => g.Id);

            // Seed data for Ledger Account, Account, and Donation
            var ledgerAccountId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            modelBuilder.Entity<LedgerAccount>().HasData(
                new LedgerAccount
                {
                    Id = ledgerAccountId,
                    AccountName = "Default Ledger",
                    AccountType = AccountTypeEnum.Checking, // Changed from string to Enum
                    CreatedBy = "System", // Required field
                    UpdatedBy = "System", // Required field
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = accountId,
                    AccountName = "Default Account",
                    AccountType = AccountTypeEnum.Checking,
                    Balance = 0
                }
            );

            modelBuilder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = Guid.NewGuid(),
                    DonorName = "John Doe",
                    Amount = 100,
                    DonationCategory = "General",
                    DonationType = "One-Time",
                    Date = DateTime.Now,
                    Phone = "123-456-7890",
                    City = "Anytown",
                    State = "Anystate",
                    Country = "Anycountry",
                    LedgerAccountId = ledgerAccountId
                }
            );

            // Configure indexes for performance
            modelBuilder.Entity<Account>().HasIndex(a => a.AccountName);
            modelBuilder.Entity<Donation>().HasIndex(d => d.DonorName);
        }
    }
}
