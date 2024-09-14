using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationCashTransactionRelation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_LedgerAccounts_LedgerAccountId",
                table: "CashTransactions");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                values: new object[] { new Guid("6674dd88-df7e-4ff2-bb0b-f37b6d678dbb"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4309), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4279), false, "System", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("4a7fc769-c05b-4d29-8074-401b33ddac16"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4340), "General", "One-Time", "John Doe", false, new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"), "123-456-7890", null, "Anystate" });

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_LedgerAccounts_LedgerAccountId",
                table: "CashTransactions",
                column: "LedgerAccountId",
                principalTable: "LedgerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_LedgerAccounts_LedgerAccountId",
                table: "CashTransactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6674dd88-df7e-4ff2-bb0b-f37b6d678dbb"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("4a7fc769-c05b-4d29-8074-401b33ddac16"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                values: new object[] { new Guid("d15ccdb1-153d-4537-88bb-b022d9aa69fe"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6630), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("93e26839-74b1-45e1-bdf8-dc82db5c3a09"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6605), false, "System", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6606) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("60f28592-663a-4bf0-910e-5563434e0ee9"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 15, 40, 42, 788, DateTimeKind.Utc).AddTicks(6667), "General", "One-Time", "John Doe", false, new Guid("93e26839-74b1-45e1-bdf8-dc82db5c3a09"), "123-456-7890", null, "Anystate" });

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_LedgerAccounts_LedgerAccountId",
                table: "CashTransactions",
                column: "LedgerAccountId",
                principalTable: "LedgerAccounts",
                principalColumn: "Id");
        }
    }
}
