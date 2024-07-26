using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnLINQ.Migrations
{
    /// <inheritdoc />
    public partial class Intitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "City", "CompanyName", "ContactName", "Country" },
                values: new object[,]
                {
                    { 1, "Obere Str. 57", "Berlin", "Alfreds Futterkiste", "Maria Anders", "Germany" },
                    { 2, "Avda. de la Constitución 2222", "México D.F.", "Ana Trujillo Emparedados y helados", "Ana Trujillo", "Mexico" },
                    { 3, "Mataderos  2312", "México D.F.", "Antonio Moreno Taquería", "Antonio Moreno", "Mexico" },
                    { 4, "120 Hanover Sq.", "London", "Around the Horn", "Thomas Hardy", "UK" },
                    { 5, "Berguvsvägen  8", "Luleå", "Berglunds snabbköp", "Christina Berglund", "Sweden" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "ProductName", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Beverages", "Chai", 19.00m },
                    { 2, "Beverages", "Chang", 19.00m },
                    { 3, "Condiments", "Aniseed Syrup", 10.00m },
                    { 4, "Condiments", "Chef Anton's Cajun Seasoning", 22.00m },
                    { 5, "Condiments", "Chef Anton's Gumbo Mix", 21.35m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "Total" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2015, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 814.50m },
                    { 2, 2, new DateTime(2015, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 228.00m },
                    { 3, 3, new DateTime(2015, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.00m },
                    { 4, 4, new DateTime(2015, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 500.00m },
                    { 5, 5, new DateTime(2015, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 10, 19.00m },
                    { 1, 2, 20, 19.00m },
                    { 2, 3, 5, 10.00m },
                    { 3, 1, 2, 19.00m },
                    { 4, 4, 10, 22.00m },
                    { 5, 5, 5, 21.35m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
