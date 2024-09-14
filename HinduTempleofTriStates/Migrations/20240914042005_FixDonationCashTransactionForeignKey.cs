using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixDonationCashTransactionForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("1ab2f2f5-ad71-41bf-a4e4-064cb6e16b90"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("9ef18f23-8da0-44f7-85ad-cff231691bd6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("499b5621-a346-401b-b8e7-620b5e6c601e"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("a7918a94-9cc2-40b9-952b-e15eb25cda70"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 4, 20, 4, 642, DateTimeKind.Utc).AddTicks(1922), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("f28db259-2734-459a-af30-07e2fac47a57"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 4, 20, 4, 642, DateTimeKind.Utc).AddTicks(1896), false, "System", new DateTime(2024, 9, 14, 4, 20, 4, 642, DateTimeKind.Utc).AddTicks(1897) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("b00750b7-efdf-493f-968d-ddc056c4e5d6"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 4, 20, 4, 642, DateTimeKind.Utc).AddTicks(1987), "General", "One-Time", "John Doe", "hindutempleoftristtate@gmail.com", false, new Guid("f28db259-2734-459a-af30-07e2fac47a57"), "123-456-7890", "D0001", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a7918a94-9cc2-40b9-952b-e15eb25cda70"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("b00750b7-efdf-493f-968d-ddc056c4e5d6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("f28db259-2734-459a-af30-07e2fac47a57"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("1ab2f2f5-ad71-41bf-a4e4-064cb6e16b90"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 4, 16, 14, 500, DateTimeKind.Utc).AddTicks(4683), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("499b5621-a346-401b-b8e7-620b5e6c601e"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 4, 16, 14, 500, DateTimeKind.Utc).AddTicks(4657), false, "System", new DateTime(2024, 9, 14, 4, 16, 14, 500, DateTimeKind.Utc).AddTicks(4658) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("9ef18f23-8da0-44f7-85ad-cff231691bd6"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 4, 16, 14, 500, DateTimeKind.Utc).AddTicks(4722), "General", "One-Time", "John Doe", "hindutempleoftristtate@gmail.com", false, new Guid("499b5621-a346-401b-b8e7-620b5e6c601e"), "123-456-7890", "D0001", "Anystate" });
        }
    }
}
