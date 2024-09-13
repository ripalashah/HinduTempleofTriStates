using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationTransactionLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions");

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

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "GeneralLedgerEntries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("62c408a1-52a5-4c02-80f1-246a5b9368bb"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 18, 31, 59, 107, DateTimeKind.Utc).AddTicks(7087), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("b9ae3ed8-dbde-4747-b307-7993daf9c154"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 18, 31, 59, 107, DateTimeKind.Utc).AddTicks(7062), false, "System", new DateTime(2024, 9, 12, 18, 31, 59, 107, DateTimeKind.Utc).AddTicks(7063) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("2531c219-d25b-4600-948c-39156162f6f6"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 18, 31, 59, 107, DateTimeKind.Utc).AddTicks(7118), "General", "One-Time", "John Doe", false, new Guid("b9ae3ed8-dbde-4747-b307-7993daf9c154"), "123-456-7890", "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                column: "GeneralLedgerEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerEntries_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries",
                column: "GeneralLedgerEntryId",
                principalTable: "GeneralLedgerEntries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerEntries_GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("62c408a1-52a5-4c02-80f1-246a5b9368bb"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("2531c219-d25b-4600-948c-39156162f6f6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("b9ae3ed8-dbde-4747-b307-7993daf9c154"));

            migrationBuilder.DropColumn(
                name: "GeneralLedgerEntryId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "GeneralLedgerEntries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id");
        }
    }
}
