using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationIdToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions");
            migrationBuilder.DropColumn(
                name: "OldColumn",
                table: "Transactions");  // Dropping a column will result in data loss
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

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("c5b14913-0187-4984-859e-abad10755bcf"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6052), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("bd18dd20-8bcb-4fb3-b03e-33cc966a874f"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6012), false, "System", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6012) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("2c12520a-413d-443f-a263-ab5c65f8cbbe"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6094), "General", "One-Time", "John Doe", false, new Guid("bd18dd20-8bcb-4fb3-b03e-33cc966a874f"), "123-456-7890", "Anystate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c5b14913-0187-4984-859e-abad10755bcf"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("2c12520a-413d-443f-a263-ab5c65f8cbbe"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("bd18dd20-8bcb-4fb3-b03e-33cc966a874f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DonationId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
