using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class GeneralLedgerEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedgerEntries_Donations_DonationId",
                table: "GeneralLedgerEntries");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("69ec8f3c-1be6-4d5a-9cc2-7272a5be3ba8"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("b4a69a07-f7dc-4a8d-ad87-247b567c7174"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("07fab9f9-55b7-4733-b0d1-8000757bac68"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e8587b0d-ea8b-48e4-b5ef-679cf80bcbc2"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 1, 11, 0, 33, DateTimeKind.Utc).AddTicks(2575), false, "System", new DateTime(2024, 9, 12, 1, 11, 0, 33, DateTimeKind.Utc).AddTicks(2576) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("0d3ed7e1-a289-47b3-bf0e-e51d537e8760"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 1, 11, 0, 33, DateTimeKind.Utc).AddTicks(2625), "General", "One-Time", "John Doe", false, new Guid("e8587b0d-ea8b-48e4-b5ef-679cf80bcbc2"), "123-456-7890", "Anystate" });

            migrationBuilder.AddForeignKey(
                name: "FK_CashTransactions_Donations_DonationId",
                table: "CashTransactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id");

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

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("07fab9f9-55b7-4733-b0d1-8000757bac68"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("0d3ed7e1-a289-47b3-bf0e-e51d537e8760"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e8587b0d-ea8b-48e4-b5ef-679cf80bcbc2"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("69ec8f3c-1be6-4d5a-9cc2-7272a5be3ba8"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(5995), false, "System", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(5996) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("b4a69a07-f7dc-4a8d-ad87-247b567c7174"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 0, 15, 57, 771, DateTimeKind.Utc).AddTicks(6039), "General", "One-Time", "John Doe", false, new Guid("e41e38a9-a91b-4c69-88fd-6ec40530789a"), "123-456-7890", "Anystate" });

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
                principalColumn: "Id");
        }
    }
}
