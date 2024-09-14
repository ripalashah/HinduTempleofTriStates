using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationMode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("fd8a0511-a4e9-4ee7-afb8-5c749b9c05fb"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("234c176d-145e-4b32-b831-abb7bff69ff6"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("87b99f49-1d3b-42fd-83eb-828bb7d534ec"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1922), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1885), false, "System", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(1886) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("f9609824-7fad-4775-a1f4-2045d3c143c1"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 14, 12, 33, 937, DateTimeKind.Utc).AddTicks(2026), "General", "One-Time", "John Doe", false, new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"), "123-456-7890", null, "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("87b99f49-1d3b-42fd-83eb-828bb7d534ec"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("f9609824-7fad-4775-a1f4-2045d3c143c1"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("fb437932-bab1-4ac9-86cb-dfa8e58cfcc5"));

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNumber",
                table: "Donations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("fd8a0511-a4e9-4ee7-afb8-5c749b9c05fb"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7939), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7914), false, "System", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7915) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "Email", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("234c176d-145e-4b32-b831-abb7bff69ff6"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 13, 52, 19, 313, DateTimeKind.Utc).AddTicks(7964), "General", "One-Time", "John Doe", "", false, new Guid("753df528-f4da-4b3c-b60e-1c187fb91778"), "123-456-7890", "", "Anystate" });
        }
    }
}
