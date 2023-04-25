using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    CredentialsId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.CredentialsId);
                    table.ForeignKey(
                        name: "FK_Clients_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "CredentialsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBooks",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBooks", x => new { x.OrderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_OrderBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBooks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "J.K.", "Rowling" },
                    { 2, "Stephen", "King" },
                    { 3, "Dan", "Brown" },
                    { 4, "Agatha", "Christie" },
                    { 5, "Jane", "Austen" },
                    { 6, "J.R.R.", "Tolkien" },
                    { 7, "Isaac", "Asimov" }
                });

            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "ClientId", "Login", "Password" },
                values: new object[,]
                {
                    { 1, null, "user1", "password1" },
                    { 2, null, "user2", "password2" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Non-fiction" },
                    { 3, "Mystery" },
                    { 4, "Fiction" },
                    { 5, "Romance" },
                    { 6, "Science Fiction" },
                    { 7, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "GenreId", "Price", "Publisher", "Quantity", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 1, 12.99m, "Scribner", 20, "The Great Gatsby", 1925 },
                    { 2, 1, 10.99m, "Bloomsbury", 19, "Harry Potter and the Philosopher's Stone", 1997 },
                    { 3, 3, 19.99m, "Doubleday", 18, "The Da Vinci Code", 2003 },
                    { 4, 3, 8.99m, "Collins Crime Club", 15, "Murder on the Orient Express", 1934 },
                    { 5, 4, 6.99m, "T. Egerton, Whitehall", 10, "Pride and Prejudice", 1813 },
                    { 7, 5, 12.99m, "Gnome Press", 11, "Foundation", 1951 },
                    { 6, 6, 15.99m, "Allen & Unwin", 13, "The Lord of the Rings", 1954 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "CredentialsId", "Email", "FirstName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe" },
                    { 2, "jane.smith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 1, 5 },
                    { 3, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Date", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 2, 15, 56, 30, 17, DateTimeKind.Local).AddTicks(5255), 15.99m, 1 },
                    { 2, 2, new DateTime(2023, 3, 2, 15, 56, 30, 19, DateTimeKind.Local).AddTicks(8145), 10.99m, 1 },
                    { 3, 2, new DateTime(2023, 3, 2, 15, 56, 30, 19, DateTimeKind.Local).AddTicks(8172), 19.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 6, 1 });

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "OrderBooks",
                columns: new[] { "BookId", "OrderId" },
                values: new object[] { 3, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBooks_BookId",
                table: "OrderBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "OrderBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Credentials");
        }
    }
}
