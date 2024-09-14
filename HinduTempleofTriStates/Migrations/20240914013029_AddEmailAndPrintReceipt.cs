using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndPrintReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("294f7435-f7b3-4ffc-82de-2f62f25a4708"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("b53ffb68-07c8-4917-a3ba-0b2927bb87fc"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("49ce3a3d-715d-4bfa-a7d6-f55ea8d52693"));

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "Donations");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "LedgerAccounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("294f7435-f7b3-4ffc-82de-2f62f25a4708"), "Default Account", 5, 0m, new DateTime(2024, 9, 13, 1, 49, 12, 999, DateTimeKind.Utc).AddTicks(5529), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("49ce3a3d-715d-4bfa-a7d6-f55ea8d52693"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 13, 1, 49, 12, 999, DateTimeKind.Utc).AddTicks(5501), false, "System", new DateTime(2024, 9, 13, 1, 49, 12, 999, DateTimeKind.Utc).AddTicks(5503) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("b53ffb68-07c8-4917-a3ba-0b2927bb87fc"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 13, 1, 49, 12, 999, DateTimeKind.Utc).AddTicks(5554), "General", "One-Time", "John Doe", false, new Guid("49ce3a3d-715d-4bfa-a7d6-f55ea8d52693"), "123-456-7890", "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                column: "GeneralLedgerEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerEntries_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                column: "GeneralLedgerEntryId",
                principalTable: "GeneralLedgerEntries",
                principalColumn: "Id");
        }
    }
}
