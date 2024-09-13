using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCascadeForGeneralLedgerEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8a8c6378-3df3-44b3-877c-06db82d3dd16"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("13b043fe-5d03-41e0-8037-5c20346e15d8"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("2226eb7c-4198-424d-89e3-107385d165f0"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("69ec8f3c-1be6-4d5a-9cc2-7272a5be3ba8"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(5995), false, "System", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(5996) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("b4a69a07-f7dc-4a8d-ad87-247b567c7174"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(6039), "General", "One-Time", "John Doe", false, new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"), "123-456-7890", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("69ec8f3c-1be6-4d5a-9cc2-7272a5be3ba8"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("b4a69a07-f7dc-4a8d-ad87-247b567c7174"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("8a8c6378-3df3-44b3-877c-06db82d3dd16"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("2226eb7c-4198-424d-89e3-107385d165f0"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5421), false, "System", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5422) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("13b043fe-5d03-41e0-8037-5c20346e15d8"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5467), "General", "One-Time", "John Doe", false, new Guid("2226eb7c-4198-424d-89e3-107385d165f0"), "123-456-7890", "Anystate" });
        }
    }
}
