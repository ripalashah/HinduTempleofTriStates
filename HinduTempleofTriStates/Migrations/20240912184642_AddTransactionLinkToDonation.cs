using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionLinkToDonation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("550c43c8-905c-4b9b-af42-06909081fd20"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 18, 46, 41, 127, DateTimeKind.Utc).AddTicks(5684), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("16468e68-f747-4b3f-aac3-09e0dea550f4"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 18, 46, 41, 127, DateTimeKind.Utc).AddTicks(5661), false, "System", new DateTime(2024, 9, 12, 18, 46, 41, 127, DateTimeKind.Utc).AddTicks(5661) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("937a69d4-e06f-4f06-aa4b-f1ef6e4de011"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 18, 46, 41, 127, DateTimeKind.Utc).AddTicks(5724), "General", "One-Time", "John Doe", false, new Guid("16468e68-f747-4b3f-aac3-09e0dea550f4"), "123-456-7890", "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DonationId",
                table: "Transactions",
                column: "DonationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DonationId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("550c43c8-905c-4b9b-af42-06909081fd20"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("937a69d4-e06f-4f06-aa4b-f1ef6e4de011"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("16468e68-f747-4b3f-aac3-09e0dea550f4"));

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "Transactions");

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
        }
    }
}
