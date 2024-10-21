using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddScopesToQuickBooksSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a343ea88-d9b5-4b89-bcd7-7d9d17f2e2d1"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("950e0da3-9f80-400f-a068-0bfaca40bf42"));

            migrationBuilder.DeleteData(
                table: "QuickBooksSettings",
                keyColumn: "Id",
                keyValue: new Guid("a5865f9d-bbbf-42e6-901f-a31454d76fbb"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("43536213-2bc1-4cca-af2f-50b8760b5d07"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("405ea349-54c8-4d5d-8bca-fbe7fb9ae961"), "Default Account", 5, 0m, new DateTime(2024, 10, 20, 0, 12, 22, 768, DateTimeKind.Utc).AddTicks(1954), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("7242714a-7e7f-4a9e-9425-ad65a6ee5771"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 10, 20, 0, 12, 22, 768, DateTimeKind.Utc).AddTicks(1926), false, "System", new DateTime(2024, 10, 20, 0, 12, 22, 768, DateTimeKind.Utc).AddTicks(1927) });

            migrationBuilder.InsertData(
                table: "QuickBooksSettings",
                columns: new[] { "Id", "AccessTokenUrl", "AuthUrl", "BaseUrl", "ClientId", "ClientSecret", "Environment", "RealmId", "RedirectUrl", "Scopes" },
                values: new object[] { new Guid("f708117b-cd69-45aa-ba2c-519f330423e9"), "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer", "https://appcenter.intuit.com/connect/oauth2", "https://sandbox-quickbooks.api.intuit.com/", "ABr6v2DHCpvpSWTW2cFS0xYCgypAWm4UpwDWt0Do64gHYztWf7", "lLWFt8xOc1MOW8Djv3hQCZwNF5DlI2BEM0JlZXG0", "sandbox", "9341453104198392", "http://ripalashah.com/htts/callback", "com.intuit.quickbooks.accounting openid profile email" });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "IsSynced", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("0d8fdb9a-5f2a-4fb9-b2e9-7cf4dd06edf5"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 10, 20, 0, 12, 22, 768, DateTimeKind.Utc).AddTicks(1994), "General", "One-Time", "John Doe", false, false, new Guid("7242714a-7e7f-4a9e-9425-ad65a6ee5771"), "123-456-7890", null, "Anystate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("405ea349-54c8-4d5d-8bca-fbe7fb9ae961"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("0d8fdb9a-5f2a-4fb9-b2e9-7cf4dd06edf5"));

            migrationBuilder.DeleteData(
                table: "QuickBooksSettings",
                keyColumn: "Id",
                keyValue: new Guid("f708117b-cd69-45aa-ba2c-519f330423e9"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("7242714a-7e7f-4a9e-9425-ad65a6ee5771"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("a343ea88-d9b5-4b89-bcd7-7d9d17f2e2d1"), "Default Account", 5, 0m, new DateTime(2024, 10, 20, 0, 7, 42, 145, DateTimeKind.Utc).AddTicks(3756), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("43536213-2bc1-4cca-af2f-50b8760b5d07"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 10, 20, 0, 7, 42, 145, DateTimeKind.Utc).AddTicks(3732), false, "System", new DateTime(2024, 10, 20, 0, 7, 42, 145, DateTimeKind.Utc).AddTicks(3732) });

            migrationBuilder.InsertData(
                table: "QuickBooksSettings",
                columns: new[] { "Id", "AccessTokenUrl", "AuthUrl", "BaseUrl", "ClientId", "ClientSecret", "Environment", "RealmId", "RedirectUrl", "Scopes" },
                values: new object[] { new Guid("a5865f9d-bbbf-42e6-901f-a31454d76fbb"), "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer", "https://appcenter.intuit.com/connect/oauth2", "https://sandbox-quickbooks.api.intuit.com/", "ABr6v2DHCpvpSWTW2cFS0xYCgypAWm4UpwDWt0Do64gHYztWf7", "lLWFt8xOc1MOW8Djv3hQCZwNF5DlI2BEM0JlZXG0", "sandbox", "9341453104198392", "http://ripalashah.com/htts/callback", "com.intuit.quickbooks.accounting openid profile email" });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "IsSynced", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("950e0da3-9f80-400f-a068-0bfaca40bf42"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 10, 20, 0, 7, 42, 145, DateTimeKind.Utc).AddTicks(3785), "General", "One-Time", "John Doe", false, false, new Guid("43536213-2bc1-4cca-af2f-50b8760b5d07"), "123-456-7890", null, "Anystate" });
        }
    }
}
