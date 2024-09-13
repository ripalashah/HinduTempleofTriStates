using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class FixTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Donations_DonationId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e3ba79f8-22b4-4991-bb3b-f924def81343"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("097bd80a-3944-4992-abff-9f53c4b6f4bd"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("13816c9c-088a-4b2c-a531-1c4bb43a1f28"));

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
                values: new object[] { new Guid("d0cb50a7-eab3-4dc8-bd12-50385623ba91"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7572), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7548), false, "System", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7548) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("10874740-41c4-42e7-afd4-19736a88ec0c"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 21, 14, 32, 685, DateTimeKind.Utc).AddTicks(7610), "General", "One-Time", "John Doe", false, new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"), "123-456-7890", "Anystate" });

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
                keyValue: new Guid("d0cb50a7-eab3-4dc8-bd12-50385623ba91"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("10874740-41c4-42e7-afd4-19736a88ec0c"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("e0b3c676-2f86-4672-bea4-bb76c8fc6db1"));

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
                values: new object[] { new Guid("e3ba79f8-22b4-4991-bb3b-f924def81343"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9902), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("13816c9c-088a-4b2c-a531-1c4bb43a1f28"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9877), false, "System", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9878) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("097bd80a-3944-4992-abff-9f53c4b6f4bd"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9929), "General", "One-Time", "John Doe", false, new Guid("13816c9c-088a-4b2c-a531-1c4bb43a1f28"), "123-456-7890", "Anystate" });

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
