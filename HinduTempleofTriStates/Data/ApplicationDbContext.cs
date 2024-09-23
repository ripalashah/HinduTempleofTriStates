using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HinduTempleofTriStates.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                // Load configuration from appsettings.json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Create DbContext options builder with SQL Server provider
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Set the provider and connection string
                optionsBuilder.UseSqlServer(connectionString);

                // Return a new ApplicationDbContext with the options
                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
        public DbSet<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OAuthToken> OAuthTokens { get; set; }
        public DbSet<QuickBooksSettings> QuickBooksSettings { get; set; }
        public DbSet<DeviceInteraction> DeviceInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global query filters for soft-deleted records
            modelBuilder.Entity<LedgerAccount>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<Donation>().HasQueryFilter(d => !d.IsDeleted && (d.LedgerAccount != null && !d.LedgerAccount.IsDeleted));
            modelBuilder.Entity<GeneralLedgerEntry>().HasQueryFilter(gl => gl.LedgerAccount != null && !gl.LedgerAccount.IsDeleted);
            modelBuilder.Entity<CashTransaction>().HasQueryFilter(ct => ct.Donation != null && !ct.Donation.IsDeleted);


            // Configure decimal precision for financial fields
            modelBuilder.Entity<Account>()
                .ToTable("Accounts")
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");
           
            modelBuilder.Entity<QuickBooksSettings>()
                .HasNoKey();

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

            // Explicitly configure Donation and CashTransaction relationship (One-to-Many)
            modelBuilder.Entity<Donation>()
                .HasMany(d => d.CashTransactions)
                .WithOne(ct => ct.Donation)
                .HasForeignKey(ct => ct.DonationId)
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure there are no conflicting properties in Donation or CashTransaction classes

            // Configure Donation and LedgerAccount relationship
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.LedgerAccount)
                .WithMany(l => l.Donations)
                .HasForeignKey(d => d.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure GeneralLedgerEntry and LedgerAccount relationship
            modelBuilder.Entity<GeneralLedgerEntry>()
                .HasOne(gl => gl.LedgerAccount)
                .WithMany(l => l.GeneralLedgerEntries)
                .HasForeignKey(gl => gl.LedgerAccountId);

            // Configure Donation and GeneralLedgerEntry relationship
            modelBuilder.Entity<Donation>()
                .HasMany(d => d.GeneralLedgerEntries)
                .WithOne(gl => gl.Donation)
                .HasForeignKey(gl => gl.DonationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Transaction and LedgerAccount relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.LedgerAccount)
                .WithMany(l => l.Transactions)
                .HasForeignKey(t => t.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // Set primary key for GeneralLedgerEntry
            modelBuilder.Entity<GeneralLedgerEntry>()
                .HasKey(g => g.Id);

            // Seed data for roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "5825bc84-9176-4167-9766-dc21abac79a1", Name = "Counter", NormalizedName = "COUNTER" },
                new IdentityRole { Id = "7cd4b244-5b57-4984-8a7c-a6d53c22772e", Name = "Accountant", NormalizedName = "ACCOUNTANT" },
                new IdentityRole { Id = "866bd64e-9f49-4e59-9616-4e460f36c0a4", Name = "Admin", NormalizedName = "ADMIN" }
            );

            // Seed default ledger account
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
                    UpdatedDate = DateTime.UtcNow,                    
                }
            );

            // Seed default account
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = Guid.NewGuid(),
                    AccountName = "Default Account",
                    AccountType = AccountTypeEnum.Checking,
                    Balance = 0
                }
            );

            // Seed default donation
            modelBuilder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = Guid.NewGuid(),
                    DonorName = "John Doe",
                    Amount = 100,
                    DonationCategory = "General",
                    DonationType = "One-Time",
                    Date = DateTime.UtcNow,
                    Phone = "123-456-7890",
                    City = "Anytown",
                    State = "Anystate",
                    Country = "Anycountry",
                    LedgerAccountId = ledgerAccountId                    
                }
            );
            modelBuilder.Entity<QuickBooksSettings>(entity =>
            {
                entity.HasKey(e => e.Id); // Ensure that Id is marked as the primary key
            });
            modelBuilder.Entity<QuickBooksSettings>().HasData(
            new QuickBooksSettings
            {
                Id = Guid.NewGuid(),
                ClientId = "ABr6v2DHCpvpSWTW2cFS0xYCgypAWm4UpwDWt0Do64gHYztWf7",
                ClientSecret = "lLWFt8xOc1MOW8Djv3hQCZwNF5DlI2BEM0JlZXG0",
                RedirectUrl = "http://ripalashah.com/htts/callback",
                Environment = "sandbox",
                AuthUrl = "https://appcenter.intuit.com/connect/oauth2",
                AccessTokenUrl = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer",
                BaseUrl = "https://sandbox-quickbooks.api.intuit.com/",
                RealmId = "9341453104198392"
            }
            );

            // Ensure soft deletion for LedgerAccount
            modelBuilder.Entity<LedgerAccount>().HasQueryFilter(l => !l.IsDeleted);
        }
    }
}