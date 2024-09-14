using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationCashTransactionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("87b99f49-1d3b-42fd-83eb-828bb7d534ec"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("f9609824-7fad-4775-a1f4-2045d3c143c1"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("d15ccdb1-153d-4537-88bb-b022d9aa69fe"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6630), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("93e26839-74b1-45e1-bdf8-dc82db5c3a09"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6605), false, "System", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6606) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("60f28592-663a-4bf0-910e-5563434e0ee9"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6667), "General", "One-Time", "John Doe", false, new Guid("93e26839-74b1-45e1-bdf8-dc82db5c3a09"), "123-456-7890", null, "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d15ccdb1-153d-4537-88bb-b022d9aa69fe"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("60f28592-663a-4bf0-910e-5563434e0ee9"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("93e26839-74b1-45e1-bdf8-dc82db5c3a09"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("87b99f49-1d3b-42fd-83eb-828bb7d534ec"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1922), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1885), false, "System", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1886) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("f9609824-7fad-4775-a1f4-2045d3c143c1"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(2026), "General", "One-Time", "John Doe", false, new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"), "123-456-7890", null, "Anystate" });
        }
    }
}
