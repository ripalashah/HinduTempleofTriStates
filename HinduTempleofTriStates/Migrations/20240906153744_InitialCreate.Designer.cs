﻿// <auto-generated />
using System;
using HinduTempleofTriStates.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240906153744_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HinduTempleofTriStates.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountName");

                    b.ToTable("Accounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3c7bf0af-fdd2-47fb-bff5-b156791277e6"),
                            AccountName = "Default Account",
                            AccountType = 5,
                            Balance = 0m,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.CashTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LedgerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LedgerAccountId");

                    b.ToTable("CashTransactions");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Donation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DonationCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("LedgerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DonorName");

                    b.HasIndex("LedgerAccountId");

                    b.ToTable("Donations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("60b2cbf1-8791-4883-bcb1-9a4fc70707e6"),
                            Amount = 100.0,
                            City = "Anytown",
                            Country = "Anycountry",
                            Date = new DateTime(2024, 9, 6, 11, 37, 42, 479, DateTimeKind.Local).AddTicks(9640),
                            DonationCategory = "General",
                            DonationType = "One-Time",
                            DonorName = "John Doe",
                            LedgerAccountId = new Guid("6cddd189-751a-4630-9542-b8ec6dda6b17"),
                            Phone = "123-456-7890",
                            State = "Anystate"
                        });
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Fund", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FundName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Funds");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.GeneralLedgerEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Debit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LedgerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LedgerAccountId");

                    b.ToTable("GeneralLedgerEntries");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.LedgerAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LedgerAccounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6cddd189-751a-4630-9542-b8ec6dda6b17"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            AccountName = "Default Ledger",
                            AccountType = 5,
                            Balance = 0m,
                            CreatedBy = "System",
                            CreatedDate = new DateTime(2024, 9, 6, 15, 37, 42, 479, DateTimeKind.Utc).AddTicks(9466),
                            UpdatedBy = "System",
                            UpdatedDate = new DateTime(2024, 9, 6, 15, 37, 42, 479, DateTimeKind.Utc).AddTicks(9467)
                        });
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("LedgerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Reconciled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReconciliationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LedgerAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.CashTransaction", b =>
                {
                    b.HasOne("HinduTempleofTriStates.Models.LedgerAccount", "LedgerAccount")
                        .WithMany("CashTransactions")
                        .HasForeignKey("LedgerAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("LedgerAccount");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Donation", b =>
                {
                    b.HasOne("HinduTempleofTriStates.Models.Account", null)
                        .WithMany("Donations")
                        .HasForeignKey("AccountId");

                    b.HasOne("HinduTempleofTriStates.Models.LedgerAccount", "LedgerAccount")
                        .WithMany("Donations")
                        .HasForeignKey("LedgerAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LedgerAccount");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.GeneralLedgerEntry", b =>
                {
                    b.HasOne("HinduTempleofTriStates.Models.LedgerAccount", "LedgerAccount")
                        .WithMany("GeneralLedgerEntries")
                        .HasForeignKey("LedgerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LedgerAccount");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Transaction", b =>
                {
                    b.HasOne("HinduTempleofTriStates.Models.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HinduTempleofTriStates.Models.LedgerAccount", "LedgerAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("LedgerAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LedgerAccount");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.Account", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("HinduTempleofTriStates.Models.LedgerAccount", b =>
                {
                    b.Navigation("CashTransactions");

                    b.Navigation("Donations");

                    b.Navigation("GeneralLedgerEntries");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
