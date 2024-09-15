using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HinduTempleofTriStates.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6674dd88-df7e-4ff2-bb0b-f37b6d678dbb"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("4a7fc769-c05b-4d29-8074-401b33ddac16"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"));

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("672b7100-76c3-4e05-b9d5-c88ca6c76d0b"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 22, 59, 57, 278, DateTimeKind.Utc).AddTicks(6858), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("a1fcf56c-84fe-4a0d-a4ca-7dc92958ed01"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 22, 59, 57, 278, DateTimeKind.Utc).AddTicks(6814), false, "System", new DateTime(2024, 9, 14, 22, 59, 57, 278, DateTimeKind.Utc).AddTicks(6815) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("146ec742-465c-4a22-a0d3-caef695d5ed4"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 22, 59, 57, 278, DateTimeKind.Utc).AddTicks(6953), "General", "One-Time", "John Doe", false, new Guid("a1fcf56c-84fe-4a0d-a4ca-7dc92958ed01"), "123-456-7890", null, "Anystate" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_ProductId",
                table: "StockTransactions",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("672b7100-76c3-4e05-b9d5-c88ca6c76d0b"));

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: new Guid("146ec742-465c-4a22-a0d3-caef695d5ed4"));

            migrationBuilder.DeleteData(
                table: "LedgerAccounts",
                keyColumn: "Id",
                keyValue: new Guid("a1fcf56c-84fe-4a0d-a4ca-7dc92958ed01"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedDate", "UpdatedDate" },
                values: new object[] { new Guid("6674dd88-df7e-4ff2-bb0b-f37b6d678dbb"), "Default Account", 5, 0m, new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4309), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LedgerAccounts",
                columns: new[] { "Id", "AccountName", "AccountType", "Balance", "CreatedBy", "CreatedDate", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"), "Default Ledger", 5, 0m, "System", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4279), false, "System", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "AccountId", "Amount", "CashTransactionId", "CashTransactionId1", "City", "Country", "Date", "DonationCategory", "DonationType", "DonorName", "IsDeleted", "LedgerAccountId", "Phone", "ReceiptNumber", "State" },
                values: new object[] { new Guid("4a7fc769-c05b-4d29-8074-401b33ddac16"), null, 100.0, null, null, "Anytown", "Anycountry", new DateTime(2024, 9, 14, 16, 8, 4, 668, DateTimeKind.Utc).AddTicks(4340), "General", "One-Time", "John Doe", false, new Guid("2e4e9680-9d01-46a2-8691-bfe9e300ddda"), "123-456-7890", null, "Anystate" });
        }
    }
}
