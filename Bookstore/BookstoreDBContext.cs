﻿using Bookstore.Entities;
using Bookstore.Helpers;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore
{
    public class BookstoreDBContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Admin> Administrators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string DatbaseLine = Config.Databaseline1;
            optionsBuilder.UseSqlServer($@"{DatbaseLine}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired();
            //modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();

            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Publisher).IsRequired();
            modelBuilder.Entity<Book>().HasOne(b => b.CoverPhoto).WithOne(p => p.Book).HasForeignKey<Photo>(p => p.BookId);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().HasOne(o=> o.Clients).WithMany(c => c.Orders).HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<OrderBook>().HasKey(ob => new { ob.OrderId, ob.BookId });
            modelBuilder.Entity<OrderBook>().HasOne(o => o.Order).WithMany(o => o.OrderBooks).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrderBook>().HasOne(b => b.Book).WithMany(b => b.OrderBooks).HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Client>().HasKey(c => c.CredentialsId);
            modelBuilder.Entity<Client>().HasOne(с => с.Credentials).WithOne(c => c.Client).HasForeignKey<Client>(c => c.CredentialsId);
            modelBuilder.Entity<Client>().Property(c => c.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Client>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Authors>().HasKey(a => a.Id);
            modelBuilder.Entity<Authors>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Authors>().Property(a => a.Surname).IsRequired();

            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.AuthorId, ba.BookId });
            modelBuilder.Entity<BookAuthor>().HasOne(a => a.Author).WithMany(a => a.BookAuthors).HasForeignKey(a => a.AuthorId);
            modelBuilder.Entity<BookAuthor>().HasOne(b => b.Book).WithMany(b => b.BookAuthors).HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookGenre>().HasKey(ba => new { ba.GenreId, ba.BookId });
            modelBuilder.Entity<BookGenre>().HasOne(a => a.Genre).WithMany(a => a.BookGenres).HasForeignKey(a => a.GenreId);
            modelBuilder.Entity<BookGenre>().HasOne(b => b.Book).WithMany(b => b.BookGenres).HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>().HasOne(c => c.Book).WithMany(b => b.Comments).HasForeignKey(c => c.BookId);
            modelBuilder.Entity<Comment>().HasOne(c => c.Client).WithMany(c => c.Comments).HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Photo>().HasKey(p => p.Id);
            modelBuilder.Entity<Photo>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Photo>().Property(p => p.ImageData).IsRequired();

            modelBuilder.Entity<Reservation>().HasKey(res => res.Id);
            modelBuilder.Entity<Reservation>().HasOne(res => res.Client).WithMany(c => c.Reservations).HasForeignKey(res => res.ClientId);
            modelBuilder.Entity<Reservation>().HasOne(res => res.Book).WithMany(b => b.Reservations).HasForeignKey(res => res.BookId);

            modelBuilder.SeedGenres();
            modelBuilder.SeedAuthors();
            modelBuilder.SeedBooks();
            modelBuilder.SeedBookGenres();
            modelBuilder.SeedBookAuthors();
            modelBuilder.SeedCredentials();
            modelBuilder.SeedClients();
            modelBuilder.SeedOrders();
            modelBuilder.SeedOrderBooks();
            modelBuilder.SeedComments();
            modelBuilder.SeedPhoto();
            modelBuilder.SeedAdmin();
            

        }


    }
}
