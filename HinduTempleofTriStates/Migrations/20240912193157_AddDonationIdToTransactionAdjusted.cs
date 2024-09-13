using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationIdToTransactionAdjusted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // migrationBuilder.DropForeignKey(
            //    name: "FK_Transactions_Donations_DonationId",
            //     table: "Transactions");
            //
            migrationBuilder.AddColumn<Guid>(
             name: "DonationId",
             table: "Transactions",
             type: "uniqueidentifier",
             nullable: true);

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
                onDelete: ReferentialAction.Restrict);

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
                values: new object[] { new Guid("e3ba79f8-22b4-4991-bb3b-f924def81343"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9902), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("13816c9c-088a-4b2c-a531-1c4bb43a1f28"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9877), false, "System", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9878) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("097bd80a-3944-4992-abff-9f53c4b6f4bd"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 19, 31, 57, 60, DateTimeKind.Utc).AddTicks(9929), "General", "One-Time", "John Doe", false, new Guid("13816c9c-088a-4b2c-a531-1c4bb43a1f28"), "123-456-7890", "Anystate" });

           
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

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "Transactions");
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
                values: new object[] { new Guid("c5b14913-0187-4984-859e-abad10755bcf"), "Default Account", 5, 0m, new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6052), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountId", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("bd18dd20-8bcb-4fb3-b03e-33cc966a874f"), new Guid("00000000-0000-0000-0000-000000000000"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6012), false, "System", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6012) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "State" },
                values: new object[] { new Guid("2c12520a-413d-443f-a263-ab5c65f8cbbe"), null, 100.0, "Anytown", "Anycountry", new DateTime(2024, 9, 12, 19, 6, 37, 892, DateTimeKind.Utc).AddTicks(6094), "General", "One-Time", "John Doe", false, new Guid("bd18dd20-8bcb-4fb3-b03e-33cc966a874f"), "123-456-7890", "Anystate" });

           
        }
    }
}
