using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnLINQ.Migrations
{
    /// <inheritdoc />
    public partial class More_Seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "Total" },
                values: new object[,]
                {
                    { 6, 3, new DateTime(2015, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 456.00m },
                    { 7, 1, new DateTime(2015, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 190.00m },
                    { 8, 4, new DateTime(2015, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1350.00m },
                    { 9, 2, new DateTime(2015, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 567.00m },
                    { 10, 3, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 945.00m },
                    { 11, 5, new DateTime(2016, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 678.00m },
                    { 12, 4, new DateTime(2016, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200.00m },
                    { 13, 1, new DateTime(2016, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 399.00m },
                    { 14, 2, new DateTime(2016, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 756.00m },
                    { 15, 3, new DateTime(2016, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 945.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 6, 1, 2, 150.00m },
                    { 6, 2, 3, 50.00m },
                    { 7, 3, 1, 190.00m },
                    { 8, 1, 5, 270.00m },
                    { 8, 4, 2, 200.00m },
                    { 9, 2, 4, 60.00m },
                    { 10, 3, 3, 210.00m },
                    { 10, 5, 1, 150.00m },
                    { 11, 2, 2, 120.00m },
                    { 11, 5, 1, 150.00m },
                    { 12, 1, 4, 300.00m },
                    { 12, 3, 2, 200.00m },
                    { 13, 4, 1, 399.00m },
                    { 14, 2, 3, 90.00m },
                    { 14, 5, 2, 180.00m },
                    { 15, 1, 3, 210.00m },
                    { 15, 3, 2, 240.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 15, 3 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15);
        }
    }
}
