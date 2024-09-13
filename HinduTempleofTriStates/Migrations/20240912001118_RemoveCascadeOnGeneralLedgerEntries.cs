using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCascadeOnGeneralLedgerEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries");

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

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("8a8c6378-3df3-44b3-877c-06db82d3dd16"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("2226eb7c-4198-424d-89e3-107385d165f0"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5421), false, "System", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5422) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("13b043fe-5d03-41e0-8037-5c20346e15d8"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 0, 11, 18, 442, DateTimeKind.Utc).AddTicks(5467), "General", "One-Time", "John Doe", false, new Guid("2226eb7c-4198-424d-89e3-107385d165f0"), "123-456-7890", "Anystate" });

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8a8c6378-3df3-44b3-877c-06db82d3dd16"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("13b043fe-5d03-41e0-8037-5c20346e15d8"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("2226eb7c-4198-424d-89e3-107385d165f0"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
