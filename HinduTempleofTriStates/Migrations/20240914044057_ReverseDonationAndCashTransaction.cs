using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class ReverseDonationAndCashTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
