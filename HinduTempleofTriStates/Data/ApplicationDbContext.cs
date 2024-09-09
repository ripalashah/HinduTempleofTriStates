﻿using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HinduTempleofTriStates.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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

            modelBuilder.Entity<Fund>()
                .Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fund>()
                .Property(f => f.Balance)
                .HasColumnType("decimal(18,2)");

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
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // Configure GeneralLedgerEntry relationships
            modelBuilder.Entity<GeneralLedgerEntry>()
                .HasKey(g => g.Id);

            // Seed data for roles only if they don't already exist
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "5825bc84-9176-4167-9766-dc21abac79a1", Name = "Counter", NormalizedName = "COUNTER" },
                new IdentityRole { Id = "7cd4b244-5b57-4984-8a7c-a6d53c22772e", Name = "Accountant", NormalizedName = "ACCOUNTANT" },
                new IdentityRole { Id = "866bd64e-9f49-4e59-9616-4e460f36c0a4", Name = "Admin", NormalizedName = "ADMIN" }
            );

            var ledgerAccountId = Guid.NewGuid();
            modelBuilder.Entity<LedgerAccount>().HasData(
                new LedgerAccount
                {
                    Id = ledgerAccountId,
                    AccountName = "Default Ledger",
                    AccountType = AccountTypeEnum.Checking,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = Guid.NewGuid(),
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

            modelBuilder.Entity<LedgerAccount>().HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
