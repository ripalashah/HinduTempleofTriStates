using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class CorrectDonationCashTransactionMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d52e7867-b121-4bfa-afd2-3711b396b91e"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("82266338-f8f9-4061-b38a-3f090259c109"));

            migrationBuilder.DeleteData(
                table: "QuickBooksSettings",
                keyColumn: "Id",
                keyValue: new Guid("253d28e2-bf2f-491f-a16c-75804eeef56e"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("82a965ac-bdb2-4ced-8484-2db559f7be51"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("70175092-f2c6-4290-aca5-de82d36e6531"), "Default Account", 5, 0m, new DateTime(2024, 9, 23, 16, 3, 29, 528, DateTimeKind.Utc).AddTicks(1163), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("cb23fb53-6c8e-48d8-9beb-abe09149f33f"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 23, 16, 3, 29, 528, DateTimeKind.Utc).AddTicks(1136), false, "System", new DateTime(2024, 9, 23, 16, 3, 29, 528, DateTimeKind.Utc).AddTicks(1138) });

            migrationBuilder.InsertData(
                table: "QuickBooksSettings",
                columns: new[] { "Id", "AccessTokenUrl", "AuthUrl", "BaseUrl", "ClientId", "ClientSecret", "Environment", "RealmId", "RedirectUrl" },
                values: new object[] { new Guid("c576cc71-c679-411c-bae8-8adfc3e9a363"), "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer", "https://appcenter.intuit.com/connect/oauth2", "https://sandbox-quickbooks.api.intuit.com/", "ABr6v2DHCpvpSWTW2cFS0xYCgypAWm4UpwDWt0Do64gHYztWf7", "lLWFt8xOc1MOW8Djv3hQCZwNF5DlI2BEM0JlZXG0", "sandbox", "9341453104198392", "http://ripalashah.com/htts/callback" });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "IsSynced", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("5e389778-9703-4b33-a6a4-6690ee21feff"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 23, 16, 3, 29, 528, DateTimeKind.Utc).AddTicks(1199), "General", "One-Time", "John Doe", false, false, new Guid("cb23fb53-6c8e-48d8-9beb-abe09149f33f"), "123-456-7890", null, "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("70175092-f2c6-4290-aca5-de82d36e6531"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("5e389778-9703-4b33-a6a4-6690ee21feff"));

            migrationBuilder.DeleteData(
                table: "QuickBooksSettings",
                keyColumn: "Id",
                keyValue: new Guid("c576cc71-c679-411c-bae8-8adfc3e9a363"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("cb23fb53-6c8e-48d8-9beb-abe09149f33f"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("d52e7867-b121-4bfa-afd2-3711b396b91e"), "Default Account", 5, 0m, new DateTime(2024, 9, 23, 15, 34, 39, 68, DateTimeKind.Utc).AddTicks(8923), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("82a965ac-bdb2-4ced-8484-2db559f7be51"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 23, 15, 34, 39, 68, DateTimeKind.Utc).AddTicks(8899), false, "System", new DateTime(2024, 9, 23, 15, 34, 39, 68, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.InsertData(
                table: "QuickBooksSettings",
                columns: new[] { "Id", "AccessTokenUrl", "AuthUrl", "BaseUrl", "ClientId", "ClientSecret", "Environment", "RealmId", "RedirectUrl" },
                values: new object[] { new Guid("253d28e2-bf2f-491f-a16c-75804eeef56e"), "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer", "https://appcenter.intuit.com/connect/oauth2", "https://sandbox-quickbooks.api.intuit.com/", "ABr6v2DHCpvpSWTW2cFS0xYCgypAWm4UpwDWt0Do64gHYztWf7", "lLWFt8xOc1MOW8Djv3hQCZwNF5DlI2BEM0JlZXG0", "sandbox", "9341453104198392", "http://ripalashah.com/htts/callback" });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "IsSynced", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("82266338-f8f9-4061-b38a-3f090259c109"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 23, 15, 34, 39, 68, DateTimeKind.Utc).AddTicks(8948), "General", "One-Time", "John Doe", false, false, new Guid("82a965ac-bdb2-4ced-8484-2db559f7be51"), "123-456-7890", null, "Anystate" });
        }
    }
}
