using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateDonationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0e83e4bf-5a47-4a65-a0d3-2723114a668b"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("9fbee19c-f8ed-49b2-8872-7757612b628e"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("dc9bcbab-473e-4959-8d45-a047eb7312a7"));

            migrationBuilder.AddColumn<Guid>(
                name: "CashTransactionId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CashTransactionId1",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("fd8a0511-a4e9-4ee7-afb8-5c749b9c05fb"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7939), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7914), false, "System", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7915) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("234c176d-145e-4b32-b831-abb7bff69ff6"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7964), "General", "One-Time", "John Doe", "", false, new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"), "123-456-7890", "", "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CashTransactionId1",
                table: "Donations",
                column: "CashTransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_CashTransactions_CashTransactionId1",
                table: "Donations",
                column: "CashTransactionId1",
                principalTable: "CashTransactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_CashTransactions_CashTransactionId1",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CashTransactionId1",
                table: "Donations");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("fd8a0511-a4e9-4ee7-afb8-5c749b9c05fb"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("234c176d-145e-4b32-b831-abb7bff69ff6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"));

            migrationBuilder.DropColumn(
                name: "CashTransactionId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "CashTransactionId1",
                table: "Donations");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("0e83e4bf-5a47-4a65-a0d3-2723114a668b"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 13, 34, 45, 653, DateTimeKind.Utc).AddTicks(5550), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("dc9bcbab-473e-4959-8d45-a047eb7312a7"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 13, 34, 45, 653, DateTimeKind.Utc).AddTicks(5518), false, "System", new DateTime(2024, 9, 14, 13, 34, 45, 653, DateTimeKind.Utc).AddTicks(5519) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("9fbee19c-f8ed-49b2-8872-7757612b628e"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 13, 34, 45, 653, DateTimeKind.Utc).AddTicks(5579), "General", "One-Time", "John Doe", "", false, new Guid("dc9bcbab-473e-4959-8d45-a047eb7312a7"), "123-456-7890", "", "Anystate" });
        }
    }
}
