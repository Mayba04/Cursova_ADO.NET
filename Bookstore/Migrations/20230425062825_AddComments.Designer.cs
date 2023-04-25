﻿// <auto-generated />
using System;
using Bookstore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bookstore.Migrations
{
    [DbContext(typeof(BookstoreDBContext))]
    [Migration("20230425062825_AddComments")]
    partial class AddComments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bookstore.Entities.Authors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "J.K.",
                            Surname = "Rowling"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Stephen",
                            Surname = "King"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dan",
                            Surname = "Brown"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Agatha",
                            Surname = "Christie"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Jane",
                            Surname = "Austen"
                        },
                        new
                        {
                            Id = 6,
                            Name = "J.R.R.",
                            Surname = "Tolkien"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Isaac",
                            Surname = "Asimov"
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 1,
                            Price = 12.99m,
                            Publisher = "Scribner",
                            Quantity = 20,
                            Title = "The Great Gatsby",
                            Year = 1925
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 1,
                            Price = 10.99m,
                            Publisher = "Bloomsbury",
                            Quantity = 19,
                            Title = "Harry Potter and the Philosopher's Stone",
                            Year = 1997
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 3,
                            Price = 19.99m,
                            Publisher = "Doubleday",
                            Quantity = 18,
                            Title = "The Da Vinci Code",
                            Year = 2003
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 3,
                            Price = 8.99m,
                            Publisher = "Collins Crime Club",
                            Quantity = 15,
                            Title = "Murder on the Orient Express",
                            Year = 1934
                        },
                        new
                        {
                            Id = 5,
                            GenreId = 4,
                            Price = 6.99m,
                            Publisher = "T. Egerton, Whitehall",
                            Quantity = 10,
                            Title = "Pride and Prejudice",
                            Year = 1813
                        },
                        new
                        {
                            Id = 6,
                            GenreId = 6,
                            Price = 15.99m,
                            Publisher = "Allen & Unwin",
                            Quantity = 13,
                            Title = "The Lord of the Rings",
                            Year = 1954
                        },
                        new
                        {
                            Id = 7,
                            GenreId = 5,
                            Price = 12.99m,
                            Publisher = "Gnome Press",
                            Quantity = 11,
                            Title = "Foundation",
                            Year = 1951
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.BookAuthor", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 4
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 5
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 5
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 6
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Client", b =>
                {
                    b.Property<int>("CredentialsId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("Status_admin")
                        .HasColumnType("bit");

                    b.HasKey("CredentialsId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            CredentialsId = 1,
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            Status_admin = false
                        },
                        new
                        {
                            CredentialsId = 2,
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            Status_admin = false
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ClientId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            ClientId = 1,
                            CreatedAt = new DateTime(2023, 4, 25, 9, 28, 24, 667, DateTimeKind.Local).AddTicks(2641),
                            Text = "Very cool book"
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            ClientId = 2,
                            CreatedAt = new DateTime(2023, 4, 25, 9, 28, 24, 667, DateTimeKind.Local).AddTicks(2911),
                            Text = "I advise you to read it"
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Credentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Credentials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "user1",
                            Password = "password1"
                        },
                        new
                        {
                            Id = 2,
                            Login = "user2",
                            Password = "password2"
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Non-fiction"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Science Fiction"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Fantasy"
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Payment_status")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientId = 1,
                            Date = new DateTime(2023, 4, 25, 9, 28, 24, 664, DateTimeKind.Local).AddTicks(1777),
                            Payment_status = false,
                            Price = 15.99m,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 2,
                            ClientId = 2,
                            Date = new DateTime(2023, 4, 25, 9, 28, 24, 666, DateTimeKind.Local).AddTicks(2565),
                            Payment_status = false,
                            Price = 10.99m,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 3,
                            ClientId = 2,
                            Date = new DateTime(2023, 4, 25, 9, 28, 24, 666, DateTimeKind.Local).AddTicks(2586),
                            Payment_status = false,
                            Price = 19.99m,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.OrderBook", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("OrderBooks");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            BookId = 6
                        },
                        new
                        {
                            OrderId = 2,
                            BookId = 2
                        },
                        new
                        {
                            OrderId = 2,
                            BookId = 3
                        });
                });

            modelBuilder.Entity("Bookstore.Entities.Book", b =>
                {
                    b.HasOne("Bookstore.Entities.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Bookstore.Entities.BookAuthor", b =>
                {
                    b.HasOne("Bookstore.Entities.Authors", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookstore.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Bookstore.Entities.Client", b =>
                {
                    b.HasOne("Bookstore.Entities.Credentials", "Credentials")
                        .WithOne("Client")
                        .HasForeignKey("Bookstore.Entities.Client", "CredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credentials");
                });

            modelBuilder.Entity("Bookstore.Entities.Comment", b =>
                {
                    b.HasOne("Bookstore.Entities.Book", "Book")
                        .WithMany("Comments")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookstore.Entities.Client", "Client")
                        .WithMany("Comments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Bookstore.Entities.Order", b =>
                {
                    b.HasOne("Bookstore.Entities.Client", "Clients")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Bookstore.Entities.OrderBook", b =>
                {
                    b.HasOne("Bookstore.Entities.Book", "Book")
                        .WithMany("OrderBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookstore.Entities.Order", "Order")
                        .WithMany("OrderBooks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Bookstore.Entities.Authors", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("Bookstore.Entities.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("Comments");

                    b.Navigation("OrderBooks");
                });

            modelBuilder.Entity("Bookstore.Entities.Client", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Bookstore.Entities.Credentials", b =>
                {
                    b.Navigation("Client");
                });

            modelBuilder.Entity("Bookstore.Entities.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Bookstore.Entities.Order", b =>
                {
                    b.Navigation("OrderBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
