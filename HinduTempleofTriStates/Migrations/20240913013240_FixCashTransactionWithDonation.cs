using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixCashTransactionWithDonation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("fe163c3b-d7be-4b47-af89-7d94e198a415"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("c40833aa-1f1a-4114-ac20-5fdfed52e6a9"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("3e0b04f4-1933-4f8d-9c49-5620eabbd5f0"));

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

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CashTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CashTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "CashTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                keyValue: new Guid("bb5be6b1-efb6-4e96-91cc-840eb94dcd4d"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("c30c4e1d-595f-4601-a2af-40c3ca6c3223"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("7a085f3c-7c25-4404-8486-737d6c3581e3"));

            migrationBuilder.DropColumn(
                name: "CashTransactionId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "CashTransactionId1",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "CashTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CashTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CashTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "CashTransactions");

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
                values: new object[] { new Guid("fe163c3b-d7be-4b47-af89-7d94e198a415"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 21, 56, 56, 60, DateTimeKind.Utc).AddTicks(4316), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("3e0b04f4-1933-4f8d-9c49-5620eabbd5f0"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 21, 56, 56, 60, DateTimeKind.Utc).AddTicks(4294), false, "System", new DateTime(2024, 9, 12, 21, 56, 56, 60, DateTimeKind.Utc).AddTicks(4294) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("c40833aa-1f1a-4114-ac20-5fdfed52e6a9"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 21, 56, 56, 60, DateTimeKind.Utc).AddTicks(4341), "General", "One-Time", "John Doe", false, new Guid("3e0b04f4-1933-4f8d-9c49-5620eabbd5f0"), "123-456-7890", "Anystate" });
        }
    }
}
