using Bookstore.Entities;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bookstore.Helpers
{
    internal static class DBInitializer
    {
        public static void SeedGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(new Genre[]
            {
                new Genre() {Id = 1, Name = "Fiction"},
                new Genre() {Id = 2, Name = "Non-fiction"},
                new Genre() {Id = 3, Name = "Mystery"},
                new Genre() {Id = 4, Name = "Epos"},
                new Genre() {Id = 5, Name = "Romance"},
                new Genre() {Id = 6, Name = "Science Fiction"},
                new Genre() {Id = 7, Name = "Fantasy"}
            });
        }

        public static void SeedBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(new Book[]
            {
            
            new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Publisher = "Scribner",
                    Year = 1925,
                    Price = 12.99m,
                    Quantity = 20,
                    ///GenreId = 1,
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Publisher = "Bloomsbury",
                    Year = 1997,
                    Price = 10.99m,
                    Quantity = 19,
                    //GenreId = 1
                },
                new Book
                {
                    Id = 3,
                    Title = "The Da Vinci Code",
                    Publisher = "Doubleday",
                    Year = 2003,
                    Price = 19.99m,
                    Quantity = 18,
                    //GenreId = 3
                },
                new Book
                {
                    Id = 4,
                    Title = "Murder on the Orient Express",
                    Publisher = "Collins Crime Club",
                    Year = 1934,
                    Price = 8.99m,
                    Quantity = 15,
                    ///GenreId = 3
                },
                new Book
                {
                    Id = 5,
                    Title = "Pride and Prejudice",
                    Publisher = "T. Egerton, Whitehall",
                    Year = 1813,
                    Price = 6.99m,
                    Quantity = 10,
                    //GenreId = 4
                },
                new Book
                {
                    Id = 6,
                    Title = "The Lord of the Rings",
                    Publisher = "Allen & Unwin",
                    Year = 1954,
                    Price = 15.99m,
                    Quantity = 13,
                    ///GenreId = 6
                },
                new Book
                {
                    Id = 7,
                    Title = "Foundation",
                    Publisher = "Gnome Press",
                    Year = 1951,
                    Price = 12.99m,
                    Quantity = 11,
                    ///GenreId = 5
                }

            });
        }

        public static void SeedAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>().HasData(new Authors[]
            {
                new Authors()
                {
                    Id = 1, Name = "J.K.", Surname = "Rowling"

                },
                new Authors()
                {
                     Id = 2, Name = "Stephen", Surname = "King"
                },
                new Authors()
                {
                     Id = 3, Name = "Dan", Surname = "Brown"
                },
                new Authors()
                {
                     Id = 4, Name = "Agatha", Surname = "Christie"
                },
                new Authors()
                {
                     Id = 5, Name = "Jane", Surname = "Austen"
                },
                new Authors()
                {
                     Id = 6, Name = "J.R.R.", Surname = "Tolkien"
                },
                new Authors()
                {
                     Id = 7, Name = "Isaac", Surname = "Asimov"
                }
            });

        }



        public static void SeedBookAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor[]
            {
                new BookAuthor() {BookId = 1, AuthorId = 1},
                new BookAuthor() {BookId = 2, AuthorId = 3},
                new BookAuthor() {BookId = 3, AuthorId = 2},
                new BookAuthor() {BookId = 4, AuthorId = 2},
                new BookAuthor() {BookId = 5, AuthorId = 1},
                new BookAuthor() {BookId = 6, AuthorId = 3},
                new BookAuthor() {BookId = 7, AuthorId = 3}
            });
        }

        public static void SeedBookGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>().HasData(new BookGenre[]
            {
                new BookGenre() {BookId = 1, GenreId = 1},
                new BookGenre() {BookId = 2, GenreId = 1},
                new BookGenre() {BookId = 3, GenreId = 3},
                new BookGenre() {BookId = 4, GenreId = 3},
                new BookGenre() {BookId = 5, GenreId = 4},
                new BookGenre() {BookId = 6, GenreId = 6},
                new BookGenre() {BookId = 7, GenreId = 6}
            });
        }

        public static void SeedCredentials(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credentials>().HasData(
                new Credentials
                {
                    Id = 1,
                    Login = "user1",
                    Password = "password1"
                },
                new Credentials
                {
                    Id = 2,
                    Login = "user2",
                    Password = "password2"
                }
               
            );
        }

        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Login = "Admin",
                    Password = "Admin123",
                    Name = "Pavlo",
                    Email = "pashamyba7@gmail.com",
                    Status_Admin = true,
                }

            );
        }


        public static void SeedClients(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    CredentialsId = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com"

                },
                new Client
                {
                    CredentialsId = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com"
                }     
            );
        }

        public static void SeedOrders(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(

            new Order { Id = 1, Date = DateTime.Now, ClientId = 1, Price = 15.99m, Quantity = 1, Payment_status = false },
            new Order { Id = 2, Date = DateTime.Now, ClientId = 2, Price = 10.99m, Quantity = 1, Payment_status = false },
            new Order { Id = 3, Date = DateTime.Now, ClientId = 2, Price = 19.99m, Quantity = 1, Payment_status = false }
            ); ;
        }

        public static void SeedOrderBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBook>().HasData(
        
            new OrderBook { OrderId = 1, BookId = 6  },
            new OrderBook { OrderId = 2, BookId = 2 },
            new OrderBook { OrderId = 2, BookId = 3 }
            );
        }

        public static void SeedComments (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(new Comment[]
            {
                new Comment() {Id = 1, Text = "Very cool book", BookId = 1, ClientId = 1, CreatedAt = DateTime.Now},
                new Comment() {Id = 2, Text = "I advise you to read it", BookId = 2, ClientId = 2, CreatedAt = DateTime.Now}
            });
        }

        public static void SeedPhoto(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>().HasData(new Photo[]
            {
                new Photo() {Id = 1, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\images.png"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\images.png"), BookId=1 },
                new Photo() {Id = 2, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=2 },
                new Photo() {Id = 3, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=3 },
                new Photo() {Id = 4, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=4 },
                new Photo() {Id = 5, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=5 },
                new Photo() {Id = 6, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=6 },
                new Photo() {Id = 7, Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"), BookId=7 },
            
            });
        }
    }
}
