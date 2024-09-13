using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCashTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f2e150d7-77d8-4f4a-936d-12d0cb6efad5"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("a2b02235-78af-4d3c-9ad4-8c8940bfceb6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("b450720d-0cd3-454c-b7a5-de11fc6e425d"));

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
                values: new object[] { new Guid("45f82b40-150f-4e53-9325-86f0fd54e52d"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(625), false, "System", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(625) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("d4886adc-fd5b-4d5f-80d4-81e2b4eb8d0c"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(673), "General", "One-Time", "John Doe", false, new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"), "123-456-7890", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("45f82b40-150f-4e53-9325-86f0fd54e52d"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("d4886adc-fd5b-4d5f-80d4-81e2b4eb8d0c"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"));

            migrationBuilder.DropColumn(
                name: "Expense",
                table: "CashTransactions");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "CashTransactions");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("f2e150d7-77d8-4f4a-936d-12d0cb6efad5"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("b450720d-0cd3-454c-b7a5-de11fc6e425d"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 1, 23, 7, 204, DateTimeKind.Utc).AddTicks(4863), false, "System", new DateTime(2024, 9, 12, 1, 23, 7, 204, DateTimeKind.Utc).AddTicks(4864) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("a2b02235-78af-4d3c-9ad4-8c8940bfceb6"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 1, 23, 7, 204, DateTimeKind.Utc).AddTicks(4911), "General", "One-Time", "John Doe", false, new Guid("b450720d-0cd3-454c-b7a5-de11fc6e425d"), "123-456-7890", "Anystate" });
        }
    }
}
