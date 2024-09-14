using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyConflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e450b634-868a-4ea8-b4c9-229ee15f81ba"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("d217c38f-6a63-46ca-bfb5-d53bc41e58b3"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("4d0ab96e-87dc-4a9d-9928-58b7996a85c3"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("e6a3706c-871c-46b0-9e8d-e6f4f8b445d3"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 4, 11, 43, 138, DateTimeKind.Utc).AddTicks(3721), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("bbf9985d-1bd6-4673-8c0e-bb98b9b0d3a8"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 4, 11, 43, 138, DateTimeKind.Utc).AddTicks(3694), false, "System", new DateTime(2024, 9, 14, 4, 11, 43, 138, DateTimeKind.Utc).AddTicks(3695) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("5e6bf2fb-0ce7-4d5e-8b65-29befbd60375"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 4, 11, 43, 138, DateTimeKind.Utc).AddTicks(3768), "General", "One-Time", "John Doe", "hindutempleoftristtate@gmail.com", false, new Guid("bbf9985d-1bd6-4673-8c0e-bb98b9b0d3a8"), "123-456-7890", "D0001", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e6a3706c-871c-46b0-9e8d-e6f4f8b445d3"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("5e6bf2fb-0ce7-4d5e-8b65-29befbd60375"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("bbf9985d-1bd6-4673-8c0e-bb98b9b0d3a8"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("e450b634-868a-4ea8-b4c9-229ee15f81ba"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 4, 7, 19, 432, DateTimeKind.Utc).AddTicks(267), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("4d0ab96e-87dc-4a9d-9928-58b7996a85c3"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 4, 7, 19, 432, DateTimeKind.Utc).AddTicks(237), false, "System", new DateTime(2024, 9, 14, 4, 7, 19, 432, DateTimeKind.Utc).AddTicks(238) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("d217c38f-6a63-46ca-bfb5-d53bc41e58b3"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 4, 7, 19, 432, DateTimeKind.Utc).AddTicks(294), "General", "One-Time", "John Doe", "hindutempleoftristtate@gmail.com", false, new Guid("4d0ab96e-87dc-4a9d-9928-58b7996a85c3"), "123-456-7890", "D0001", "Anystate" });
        }
    }
}
