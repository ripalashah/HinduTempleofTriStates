using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadingDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
