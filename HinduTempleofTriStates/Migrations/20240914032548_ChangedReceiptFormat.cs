using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class ChangedReceiptFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("edf491a5-6870-4e66-b011-aaba76a2cf18"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("004f2167-c830-447d-9371-0ec9771ac11a"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("7eea5777-d105-4c78-b532-16850aaa59ef"));

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("edf491a5-6870-4e66-b011-aaba76a2cf18"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 1, 30, 27, 684, DateTimeKind.Utc).AddTicks(5326), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("7eea5777-d105-4c78-b532-16850aaa59ef"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 1, 30, 27, 684, DateTimeKind.Utc).AddTicks(5305), false, "System", new DateTime(2024, 9, 14, 1, 30, 27, 684, DateTimeKind.Utc).AddTicks(5306) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("004f2167-c830-447d-9371-0ec9771ac11a"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 1, 30, 27, 684, DateTimeKind.Utc).AddTicks(5353), "General", "One-Time", "John Doe", "hindutempleoftristtates@yahoo.com", false, new Guid("7eea5777-d105-4c78-b532-16850aaa59ef"), "123-456-7890", 0, "Anystate" });
        }
    }
}
