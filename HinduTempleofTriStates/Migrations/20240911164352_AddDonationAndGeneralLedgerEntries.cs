using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationAndGeneralLedgerEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "LedgerAccountId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
