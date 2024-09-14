using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixDonationCashTransactionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("86c333e8-108b-4aa0-9e10-965799a0b106"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("edd03714-51d9-433f-8fba-bc401081d175"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("ae448669-7f36-435e-8c72-4c817606cd9e"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("86c333e8-108b-4aa0-9e10-965799a0b106"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 3, 25, 46, 755, DateTimeKind.Utc).AddTicks(1126), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("ae448669-7f36-435e-8c72-4c817606cd9e"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 3, 25, 46, 755, DateTimeKind.Utc).AddTicks(1075), false, "System", new DateTime(2024, 9, 14, 3, 25, 46, 755, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("edd03714-51d9-433f-8fba-bc401081d175"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 3, 25, 46, 755, DateTimeKind.Utc).AddTicks(1170), "General", "One-Time", "John Doe", "hindutempleoftristtate@gmail.com", false, new Guid("ae448669-7f36-435e-8c72-4c817606cd9e"), "123-456-7890", "D0001", "Anystate" });
        }
    }
}
