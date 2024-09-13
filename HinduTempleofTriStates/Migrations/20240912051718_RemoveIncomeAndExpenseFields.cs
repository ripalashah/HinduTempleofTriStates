using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIncomeAndExpenseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2feae16b-1c72-4e4a-9943-d371c1750880"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("790e858b-33fe-4521-a199-3f8dbc82aa17"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("70d0d942-936e-4a64-9ad2-22d4ec4feb10"));

            migrationBuilder.DropColumn(
                name: "Expense",
                table: "CashTransactions");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "CashTransactions");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("5ed72c95-0ba7-4a36-b5c2-d588ca1ead33"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("16c83892-8cb1-42f2-ab78-a6c5b68e073d"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 5, 17, 16, 808, DateTimeKind.Utc).AddTicks(7202), false, "System", new DateTime(2024, 9, 12, 5, 17, 16, 808, DateTimeKind.Utc).AddTicks(7202) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("7af572ad-5244-45a0-b9a7-086e7185eed2"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 5, 17, 16, 808, DateTimeKind.Utc).AddTicks(7270), "General", "One-Time", "John Doe", false, new Guid("16c83892-8cb1-42f2-ab78-a6c5b68e073d"), "123-456-7890", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5ed72c95-0ba7-4a36-b5c2-d588ca1ead33"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("7af572ad-5244-45a0-b9a7-086e7185eed2"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("16c83892-8cb1-42f2-ab78-a6c5b68e073d"));

            migrationBuilder.AddColumn<double>(
                name: "Expense",
                table: "CashTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Income",
                table: "CashTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("2feae16b-1c72-4e4a-9943-d371c1750880"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("70d0d942-936e-4a64-9ad2-22d4ec4feb10"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 4, 55, 35, 839, DateTimeKind.Utc).AddTicks(1672), false, "System", new DateTime(2024, 9, 12, 4, 55, 35, 839, DateTimeKind.Utc).AddTicks(1673) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("790e858b-33fe-4521-a199-3f8dbc82aa17"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 4, 55, 35, 839, DateTimeKind.Utc).AddTicks(1716), "General", "One-Time", "John Doe", false, new Guid("70d0d942-936e-4a64-9ad2-22d4ec4feb10"), "123-456-7890", "Anystate" });
        }
    }
}
