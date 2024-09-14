using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyForDonationAndCashTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
