using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Migrations
{
    public partial class AddedSeedBookGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 4 },
                    { 6, 6 },
                    { 7, 6 }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 5, 16, 56, 6, 355, DateTimeKind.Local).AddTicks(2672));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 5, 16, 56, 6, 355, DateTimeKind.Local).AddTicks(2927));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 56, 6, 352, DateTimeKind.Local).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 56, 6, 354, DateTimeKind.Local).AddTicks(7851));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 56, 6, 354, DateTimeKind.Local).AddTicks(7874));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 5, 16, 30, 51, 678, DateTimeKind.Local).AddTicks(9928));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 5, 16, 30, 51, 679, DateTimeKind.Local).AddTicks(167));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 30, 51, 676, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 30, 51, 678, DateTimeKind.Local).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 6, 5, 16, 30, 51, 678, DateTimeKind.Local).AddTicks(5523));
        }
    }
}
