using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateCashTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("45f82b40-150f-4e53-9325-86f0fd54e52d"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("d4886adc-fd5b-4d5f-80d4-81e2b4eb8d0c"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("46794c5e-93fc-45c2-ad74-fe860c419065"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("670b08b4-f185-40b4-9f08-09ff1951342b"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 4, 49, 17, 509, DateTimeKind.Utc).AddTicks(2157), false, "System", new DateTime(2024, 9, 12, 4, 49, 17, 509, DateTimeKind.Utc).AddTicks(2157) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("f0b113da-6552-4ede-8381-8edc5b2c1570"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 4, 49, 17, 509, DateTimeKind.Utc).AddTicks(2252), "General", "One-Time", "John Doe", false, new Guid("670b08b4-f185-40b4-9f08-09ff1951342b"), "123-456-7890", "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("46794c5e-93fc-45c2-ad74-fe860c419065"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("f0b113da-6552-4ede-8381-8edc5b2c1570"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("670b08b4-f185-40b4-9f08-09ff1951342b"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "UpdatedDate" },
                values: new object[] { new Guid("45f82b40-150f-4e53-9325-86f0fd54e52d"), "Default Account", 5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(625), false, "System", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(625) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("d4886adc-fd5b-4d5f-80d4-81e2b4eb8d0c"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 4, 40, 10, 17, DateTimeKind.Utc).AddTicks(673), "General", "One-Time", "John Doe", false, new Guid("a456fffc-93b9-4124-91bc-ecf6ed75a4b2"), "123-456-7890", "Anystate" });
        }
    }
}
