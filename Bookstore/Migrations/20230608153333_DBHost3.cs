using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Migrations
{
    public partial class DBHost3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderBooks",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 2, 3 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 33, 32, 896, DateTimeKind.Local).AddTicks(465), true });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(709), true });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(731), true });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Date", "Payment_status", "Price", "Quantity" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(734), true, 8.99m, 1 },
                    { 5, 1, new DateTime(2023, 6, 8, 18, 33, 32, 898, DateTimeKind.Local).AddTicks(736), true, 6.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 5, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderBooks",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "OrderBooks",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "OrderBooks",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 8, 18, 22, 6, 653, DateTimeKind.Local).AddTicks(6820));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 8, 18, 22, 6, 653, DateTimeKind.Local).AddTicks(7196));

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 22, 6, 651, DateTimeKind.Local).AddTicks(2535), false });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 22, 6, 653, DateTimeKind.Local).AddTicks(2424), false });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Payment_status" },
                values: new object[] { new DateTime(2023, 6, 8, 18, 22, 6, 653, DateTimeKind.Local).AddTicks(2445), false });
        }
    }
}
