using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f73696fc-e265-4570-82cb-cbad800c7a49"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("db66dee4-693c-4c1e-b3a2-6c6489fab96b"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("0655452b-fe90-418d-9218-6f2e136a6f82"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("7f149d60-faa1-463b-90d6-341d4f22443b"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("c2bf913b-d14c-4700-8f07-e75646aab9b2"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 10, 23, 17, 33, 757, DateTimeKind.Utc).AddTicks(4980), false, "System", new DateTime(2024, 9, 10, 23, 17, 33, 757, DateTimeKind.Utc).AddTicks(4981) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("b2e435c0-c7ee-48b6-8b54-399232461f95"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 10, 19, 17, 33, 757, DateTimeKind.Local).AddTicks(5032), "General", "One-Time", "John Doe", false, new Guid("c2bf913b-d14c-4700-8f07-e75646aab9b2"), "123-456-7890", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7f149d60-faa1-463b-90d6-341d4f22443b"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("b2e435c0-c7ee-48b6-8b54-399232461f95"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("c2bf913b-d14c-4700-8f07-e75646aab9b2"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("f73696fc-e265-4570-82cb-cbad800c7a49"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("0655452b-fe90-418d-9218-6f2e136a6f82"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 9, 3, 20, 13, 781, DateTimeKind.Utc).AddTicks(575), false, "System", new DateTime(2024, 9, 9, 3, 20, 13, 781, DateTimeKind.Utc).AddTicks(576) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("db66dee4-693c-4c1e-b3a2-6c6489fab96b"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 8, 23, 20, 13, 781, DateTimeKind.Local).AddTicks(627), "General", "One-Time", "John Doe", false, new Guid("0655452b-fe90-418d-9218-6f2e136a6f82"), "123-456-7890", "Anystate" });
        }
    }
}
