using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixCashTransactionMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bb5be6b1-efb6-4e96-91cc-840eb94dcd4d"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("c30c4e1d-595f-4601-a2af-40c3ca6c3223"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("7a085f3c-7c25-4404-8486-737d6c3581e3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("bb5be6b1-efb6-4e96-91cc-840eb94dcd4d"), "Default Account", 5, 0m, new DateTime(2024, 9, 13, 1, 32, 38, 818, DateTimeKind.Utc).AddTicks(6096), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("7a085f3c-7c25-4404-8486-737d6c3581e3"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 13, 1, 32, 38, 818, DateTimeKind.Utc).AddTicks(6073), false, "System", new DateTime(2024, 9, 13, 1, 32, 38, 818, DateTimeKind.Utc).AddTicks(6074) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("c30c4e1d-595f-4601-a2af-40c3ca6c3223"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 13, 1, 32, 38, 818, DateTimeKind.Utc).AddTicks(6122), "General", "One-Time", "John Doe", false, new Guid("7a085f3c-7c25-4404-8486-737d6c3581e3"), "123-456-7890", "Anystate" });
        }
    }
}
