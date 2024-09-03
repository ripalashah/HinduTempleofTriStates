﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<Donation> Donations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LedgerAccount>()
                .Property(l => l.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            // Donations to LedgerAccount
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.LedgerAccount)
                .WithMany(l => l.Donations)
                .HasForeignKey(d => d.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transactions to LedgerAccount
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.LedgerAccount)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // CashTransactions to LedgerAccount
            modelBuilder.Entity<CashTransaction>()
                .HasOne(ct => ct.LedgerAccount)
                .WithMany(l => l.CashTransactions)
                .HasForeignKey(ct => ct.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // GeneralLedgerEntry EntryId Configuration
            modelBuilder.Entity<GeneralLedgerEntry>().HasNoKey()
                .Property(g => g.EntryId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v))
                .HasColumnName("EntryId");
        }
    }
}
