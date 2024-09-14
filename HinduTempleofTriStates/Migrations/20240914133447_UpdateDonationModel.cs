using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: new Guid("92a54044-ee9a-41e9-9e8f-1237846e38e0"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("d8352632-d646-471d-892b-ce4d65862cd3"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("97b0655e-4604-4ba6-85a2-f64d1a5ec3f2"));

            migrationBuilder.DropColumn(
                name: "CashTransactionId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "CashTransactionId1",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

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
                values: new object[] { new Guid("92a54044-ee9a-41e9-9e8f-1237846e38e0"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 4, 40, 55, 706, DateTimeKind.Utc).AddTicks(9557), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("97b0655e-4604-4ba6-85a2-f64d1a5ec3f2"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 4, 40, 55, 706, DateTimeKind.Utc).AddTicks(9489), false, "System", new DateTime(2024, 9, 14, 4, 40, 55, 706, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("d8352632-d646-471d-892b-ce4d65862cd3"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 4, 40, 55, 706, DateTimeKind.Utc).AddTicks(9581), "General", "One-Time", "John Doe", false, new Guid("97b0655e-4604-4ba6-85a2-f64d1a5ec3f2"), "123-456-7890", null, "Anystate" });

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
    }
}
