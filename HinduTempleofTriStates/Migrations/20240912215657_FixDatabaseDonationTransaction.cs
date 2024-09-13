using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabaseDonationTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d0cb50a7-eab3-4dc8-bd12-50385623ba91"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("10874740-41c4-42e7-afd4-19736a88ec0c"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("d0cb50a7-eab3-4dc8-bd12-50385623ba91"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7572), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7548), false, "System", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7548) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("10874740-41c4-42e7-afd4-19736a88ec0c"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7610), "General", "One-Time", "John Doe", false, new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"), "123-456-7890", "Anystate" });
        }
    }
}
