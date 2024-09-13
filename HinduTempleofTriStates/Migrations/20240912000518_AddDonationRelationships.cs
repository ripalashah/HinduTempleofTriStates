using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d9240d0e-6b39-4bdd-b422-ed978e7a9b22"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("1bf52093-b593-455f-97af-23c679fca081"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e7eca47e-55be-4fd0-94ff-8a44036541a1"));

            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "GeneralLedgerEntries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "CashTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("8523d208-4832-4944-9977-cb8b09d1cda4"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("1a6b817b-328b-4df4-a531-f2a204958958"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 0, 5, 17, 526, DateTimeKind.Utc).AddTicks(4298), false, "System", new DateTime(2024, 9, 12, 0, 5, 17, 526, DateTimeKind.Utc).AddTicks(4299) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("a92038cb-0d11-496d-ab92-5db938801b29"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 0, 5, 17, 526, DateTimeKind.Utc).AddTicks(4358), "General", "One-Time", "John Doe", false, new Guid("1a6b817b-328b-4df4-a531-f2a204958958"), "123-456-7890", "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerEntries_DonationId",
                table: "GeneralLedgerEntries",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_CashTransactions_DonationId",
                table: "CashTransactions",
                column: "DonationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedgerEntries_DonationId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropIndex(
                name: "IX_CashTransactions_DonationId",
                table: "CashTransactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8523d208-4832-4944-9977-cb8b09d1cda4"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("a92038cb-0d11-496d-ab92-5db938801b29"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("1a6b817b-328b-4df4-a531-f2a204958958"));

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "CashTransactions");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("d9240d0e-6b39-4bdd-b422-ed978e7a9b22"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e7eca47e-55be-4fd0-94ff-8a44036541a1"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 11, 16, 43, 51, 239, DateTimeKind.Utc).AddTicks(2484), false, "System", new DateTime(2024, 9, 11, 16, 43, 51, 239, DateTimeKind.Utc).AddTicks(2485) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("1bf52093-b593-455f-97af-23c679fca081"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 11, 12, 43, 51, 239, DateTimeKind.Local).AddTicks(2530), "General", "One-Time", "John Doe", false, new Guid("e7eca47e-55be-4fd0-94ff-8a44036541a1"), "123-456-7890", "Anystate" });
        }
    }
}
