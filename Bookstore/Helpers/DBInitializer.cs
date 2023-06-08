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
                new Genre() {Id = 1, Name = "Classic"},
                new Genre() {Id = 2, Name = "Fantasy"},
                new Genre() {Id = 3, Name = "Mystery"},
                new Genre() {Id = 4, Name = "Romance"},
                new Genre() {Id = 5, Name = "Science Fiction"},
                new Genre() {Id = 6, Name = "Dystopian"},
                new Genre() {Id = 7, Name = "Adventure"},
                new Genre() {Id = 8, Name = "Epic"},
                new Genre() {Id = 9, Name = "Modernist"},
                new Genre() {Id = 10, Name = "Gothic"},
                new Genre() {Id = 11, Name = "Memoir"},
                new Genre() {Id = 12, Name = "Psychological Thriller"},
                new Genre() {Id = 13, Name = "Historical Fiction"},
                new Genre() {Id = 14, Name = "Thriller"},
                new Genre() {Id = 15, Name = "Young Adult"},
                new Genre() {Id = 16, Name = "Crime Fiction"},
                new Genre() {Id = 17, Name = "Fiction"},
                new Genre() {Id = 18, Name = "Contemporary Fiction"},
                new Genre() {Id = 19, Name = "Dystopian Fiction"},
                new Genre() {Id = 20, Name = "Psychological Thriller"},
                new Genre() {Id = 21, Name = "Romantic Comedy"},
                new Genre() {Id = 22, Name = "Non-fiction"}
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
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Publisher = "Bloomsbury",
                    Year = 1997,
                    Price = 10.99m,
                    Quantity = 19,
                },
                new Book
                {
                    Id = 3,
                    Title = "The Da Vinci Code",
                    Publisher = "Doubleday",
                    Year = 2003,
                    Price = 19.99m,
                    Quantity = 18,
                },
                new Book
                {
                    Id = 4,
                    Title = "Murder on the Orient Express",
                    Publisher = "Collins Crime Club",
                    Year = 1934,
                    Price = 8.99m,
                    Quantity = 15,
                },
                new Book
                {
                    Id = 5,
                    Title = "Pride and Prejudice",
                    Publisher = "T. Egerton, Whitehall",
                    Year = 1813,
                    Price = 6.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 6,
                    Title = "The Lord of the Rings",
                    Publisher = "Allen & Unwin",
                    Year = 1954,
                    Price = 15.99m,
                    Quantity = 13,
                },
                new Book
                {
                    Id = 7,
                    Title = "Foundation",
                    Publisher = "Gnome Press",
                    Year = 1951,
                    Price = 12.99m,
                    Quantity = 11,
                },
                new Book
                {
                    Id = 8,
                    Title = "To Kill a Mockingbird",
                    Publisher = "J. B. Lippincott & Co.",
                    Year = 1960,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 9,
                    Title = "Nineteen Eighty-Four",
                    Publisher = "Penguin Books",
                    Year = 1949,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 10,
                    Title = "Crime and Punishment",
                    Publisher = "The Messenger",
                    Year = 1866,
                    Price = 14.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 11,
                    Title = "Moby-Dick",
                    Publisher = "Richard Bentley",
                    Year = 1851,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 12,
                    Title = "The Catcher in the Rye",
                    Publisher = "Little, Brown and Company",
                    Year = 1951,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 13,
                    Title = "The Hobbit",
                    Publisher = "George Allen & Unwin",
                    Year = 1937,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 14,
                    Title = "Don Quixote",
                    Publisher = "Francisco de Robles",
                    Year = 1605,
                    Price = 15.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 15,
                    Title = "Jane Eyre",
                    Publisher = "Smith, Elder & Co.",
                    Year = 1847,
                    Price = 10.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 16,
                    Title = "The Odyssey",
                    Publisher = "Unknown",
                    Year = 8,
                    Price = 70.99m,
                    Quantity = 1,
                },
                new Book
                {
                    Id = 17,
                    Title = "The Great Expectations",
                    Publisher = "Chapman & Hall",
                    Year = 1861,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 18,
                    Title = "The Chronicles of Narnia",
                    Publisher = "Geoffrey Bles",
                    Year = 1950,
                    Price = 13.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 19,
                    Title = "The Alchemist",
                    Publisher = "HarperOne",
                    Year = 1988,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 20,
                    Title = "To Kill",
                    Publisher = "J. B. Lippincott & Co.",
                    Year = 1960,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 21,
                    Title = "The Catcher in the Rye",
                    Publisher = "Little, Brown and Company",
                    Year = 1951,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 22,
                    Title = "To the Lighthouse",
                    Publisher = "Hogarth Press",
                    Year = 1927,
                    Price = 10.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 23,
                    Title = "Lord of the Flies",
                    Publisher = "Faber and Faber",
                    Year = 1954,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 24,
                    Title = "The Grapes of Wrath",
                    Publisher = "The Viking Press",
                    Year = 1939,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 25,
                    Title = "The Picture of Dorian Gray",
                    Publisher = "Lippincott's Monthly Magazine",
                    Year = 1890,
                    Price = 8.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 26,
                    Title = "Educated",
                    Publisher = "Random House",
                    Year = 2018,
                    Price = 14.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 27,
                    Title = "Becoming",
                    Publisher = "Crown Publishing Group",
                    Year = 2018,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 28,
                    Title = "The Silent Patient",
                    Publisher = "Celadon Books",
                    Year = 2019,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 29,
                    Title = "Where the Crawdads Sing",
                    Publisher = "G.P. Putnam's Sons",
                    Year = 2018,
                    Price = 13.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 30,
                    Title = "The Tattooist of Auschwitz",
                    Publisher = "HarperCollins",
                    Year = 2018,
                    Price = 12.99m,
                    Quantity = 10,
                },

                new Book
                {
                    Id = 31,
                    Title = "The Girl on the Train",
                    Publisher = "Riverhead Books",
                    Year = 2015,
                    Price = 10.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 32,
                    Title = "Gone Girl",
                    Publisher = "Crown Publishing Group",
                    Year = 2012,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 33,
                    Title = "The Fault in Our Stars",
                    Publisher = "Dutton Books",
                    Year = 2012,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 34,
                    Title = "The Hunger Games",
                    Publisher = "Scholastic Corporation",
                    Year = 2008,
                    Price = 8.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 35,
                    Title = "The Help",
                    Publisher = "Amy Einhorn Books",
                    Year = 2009,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 36,
                    Title = "A Game of Thrones",
                    Publisher = "Bantam Spectra",
                    Year = 1996,
                    Price = 14.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 37,
                    Title = "The Kite Runner",
                    Publisher = "Riverhead Books",
                    Year = 2003,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 38,
                    Title = "The Girl with the Dragon Tattoo",
                    Publisher = "Norstedts Förlag",
                    Year = 2005,
                    Price = 10.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 39,
                    Title = "The Maze Runner",
                    Publisher = "Delacorte Press",
                    Year = 2009,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 40,
                    Title = "The Book Thief",
                    Publisher = "Knopf Books",
                    Year = 2005,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 41,
                    Title = "The Girl with the Lower Back Tattoo",
                    Publisher = "Gallery Books",
                    Year = 2016,
                    Price = 13.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 42,
                    Title = "Circe",
                    Publisher = "Little, Brown and Company",
                    Year = 2018,
                    Price = 13.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 43,
                    Title = "The Underground Railroad",
                    Publisher = "Doubleday",
                    Year = 2016,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 44,
                    Title = "The Girl on the Dock",
                    Publisher = "Self-published",
                    Year = 2014,
                    Price = 9.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 45,
                    Title = "The Immortalists",
                    Publisher = "G.P. Putnam's Sons",
                    Year = 2018,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 46,
                    Title = "Little Fires Everywhere",
                    Publisher = "Penguin Press",
                    Year = 2017,
                    Price = 10.99m,
                    Quantity = 10,
                },

                 new Book
                {
                    Id = 47,
                    Title = "Eleanor Oliphant Is Completely Fine",
                    Publisher = "HarperCollins",
                    Year = 2017,
                    Price = 12.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 48,
                    Title = "The Nightingale",
                    Publisher = "St. Martin's Press",
                    Year = 2015,
                    Price = 11.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 49,
                    Title = "Normal People",
                    Publisher = "Hogarth",
                    Year = 2018,
                    Price = 10.99m,
                    Quantity = 10,
                },
                new Book
                {
                    Id = 50,
                    Title = "Big Little Lies",
                    Publisher = "G.P. Putnam's Sons",
                    Year = 2014,
                    Price = 13.99m,
                    Quantity = 10,
                },
                //new Book
                //{
                //    Id = 51,
                //    Title = "The Goldfinch",
                //    Publisher = "Little, Brown and Company",
                //    Year = 2013,
                //    Price = 14.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 52,
                //    Title = "The Handmaid's Tale",
                //    Publisher = "McClelland & Stewart",
                //    Year = 1985,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 53,
                //    Title = "Crazy Rich Asians",
                //    Publisher = "Doubleday",
                //    Year = 2013,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 54,
                //    Title = "A Little Life",
                //    Publisher = "Doubleday",
                //    Year = 2015,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 55,
                //    Title = "The Silent Wife",
                //    Publisher = "Orion Publishing Group",
                //    Year = 2013,
                //    Price = 9.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 56,
                //    Title = "The Girl with All the Gifts",
                //    Publisher = "Orbit Books",
                //    Year = 2014,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 57,
                //    Title = "The Night Circus",
                //    Publisher = "Doubleday",
                //    Year = 2011,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 58,
                //    Title = "All the Light We Cannot See",
                //    Publisher = "Scribner",
                //    Year = 2014,
                //    Price = 13.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 59,
                //    Title = "The Rosie Project",
                //    Publisher = "Text Publishing",
                //    Year = 2013,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 60,
                //    Title = "The Martian",
                //    Publisher = "Crown Publishing Group",
                //    Year = 2011,
                //    Price = 9.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 61,
                //    Title = "Station Eleven",
                //    Publisher = "Knopf",
                //    Year = 2014,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 62,
                //    Title = "American Gods",
                //    Publisher = "William Morrow",
                //    Year = 2001,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 63,
                //    Title = "Ready Player One",
                //    Publisher = "Crown Publishing Group",
                //    Year = 2011,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 64,
                //    Title = "Eleanor & Park",
                //    Publisher = "St. Martin's Press",
                //    Year = 2013,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 65,
                //    Title = "The Book of Lost Things",
                //    Publisher = "John Murray",
                //    Year = 2006,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 66,
                //    Title = "The Immortal Life of Henrietta Lacks",
                //    Publisher = "Crown Publishing Group",
                //    Year = 2010,
                //    Price = 13.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 67,
                //    Title = "The Light Between Oceans",
                //    Publisher = "Scribner",
                //    Year = 2012,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 68,
                //    Title = "Before We Were Strangers",
                //    Publisher = "Atria Books",
                //    Year = 2015,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 69,
                //    Title = "The Great Alone",
                //    Publisher = "St. Martin's Press",
                //    Year = 2018,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 70,
                //    Title = "The Hate U Give",
                //    Publisher = "Balzer + Bray",
                //    Year = 2017,
                //    Price = 13.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 71,
                //    Title = "The Woman in the Window",
                //    Publisher = "William Morrow",
                //    Year = 2018,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 72,
                //    Title = "An American Marriage",
                //    Publisher = "Algonquin Books",
                //    Year = 2018,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 73,
                //    Title = "Pachinko",
                //    Publisher = "Grand Central Publishing",
                //    Year = 2017,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 74,
                //    Title = "The Testaments",
                //    Publisher = "Nan A. Talese",
                //    Year = 2019,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 75,
                //    Title = "Where the Forest Meets the Stars",
                //    Publisher = "Lake Union Publishing",
                //    Year = 2019,
                //    Price = 11.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 76,
                //    Title = "The Dutch House",
                //    Publisher = "Harper",
                //    Year = 2019,
                //    Price = 13.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 77,
                //    Title = "1984",
                //    Publisher = "Secker & Warburg",
                //    Year = 1949,
                //    Price = 8.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 78,
                //    Title = "Brave New World",
                //    Publisher = "Chatto & Windus",
                //    Year = 1932,
                //    Price = 10.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 79,
                //    Title = "The Divine Comedy",
                //    Publisher = "Dante Alighieri",
                //    Year = 1320,
                //    Price = 12.99m,
                //    Quantity = 10,
                //},
                //new Book
                //{
                //    Id = 80,
                //    Title = "Les Misérables",
                //    Publisher = "A. Lacroix, Verboeckhoven & Cie",
                //    Year = 1862,
                //    Price = 11.99m,
                //    Quantity = 10,
                //}
            });
        }

        public static void SeedAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>().HasData(new Authors[]
            {
                new Authors()
                {
                    Id = 1, Name = "F. Scott", Surname = "Fitzgerald"
                },
                new Authors()
                {
                     Id = 2, Name = "J.K.", Surname = "Rowling"
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
                },
                 new Authors
                {
                    Id = 8, Name = "Harper", Surname = "Lee"
                },
                new Authors
                {
                    Id = 9, Name = "George", Surname = "Orwell"
                },
                new Authors
                {
                    Id = 10, Name = "Fyodor", Surname = "Dostoevsky"
                },
                new Authors
                {
                    Id = 11, Name = "Herman", Surname = "Melville"
                },
                new Authors
                {
                    Id = 12, Name = "J.D.", Surname = "Salinger"
                },
                new Authors
                {
                    Id = 13, Name = "Miguel", Surname = "Cervantes"
                },
                new Authors
                {
                    Id = 14, Name = "Charlotte", Surname = "Brontë"
                },
                new Authors
                {
                    Id = 15, Name = "Homer", Surname = ""
                },
                new Authors
                {
                    Id = 16, Name = "Charles",Surname = "Dickens"
                },
                new Authors
                {
                    Id = 17, Name = "C.S.", Surname = "Lewis"
                }, 
                new Authors
                {
                    Id = 18, Name = "Paulo", Surname = "Coelho"
                }, 
                new Authors
                {
                    Id = 19, Name = "Virginia", Surname = "Woolf"
                },
                new Authors
                {
                    Id = 20, Name = "William",  Surname = "Golding"
                },
                new Authors
                {
                    Id = 21, Name = "John", Surname = "Steinbeck"
                }, 
                new Authors
                {
                    Id = 22, Name = "Oscar", Surname = "Wilde"
                }, 
                new Authors
                {
                    Id = 23, Name = "Tara", Surname = "Westover"
                },
                new Authors
                {
                    Id = 24, Name = "Michelle", Surname = "Obama"
                },
                new Authors
                {
                    Id = 25, Name = "Alex", Surname = "Michaelides"
                },  
                new Authors
                {
                    Id = 26, Name = "Delia", Surname = "Owens"
                }, 
                new Authors
                {
                    Id = 27, Name = "Heather", Surname = "Morris"
                },
                new Authors
                {
                    Id = 28, Name = "Paula", Surname = "Hawkins"
                },
                new Authors
                {
                    Id = 29, Name = "Gillian", Surname = "Flynn"
                },
                new Authors
                {
                    Id = 30, Name = "John", Surname = "Green"
                },
                new Authors
                {
                    Id = 31, Name = "Suzanne", Surname = "Collins"
                },
                new Authors
                {
                    Id = 32, Name = "Kathryn", Surname = "Stockett"
                },
                new Authors
                {
                    Id = 33, Name = "George R.R.", Surname = "Martin"
                },
                new Authors
                {
                    Id = 34, Name = "Khaled", Surname = "Hosseini"
                },
                new Authors
                {
                    Id = 35, Name = "Stieg",  Surname = "Larsson"
                },
                new Authors
                {
                    Id = 36, Name = "James", Surname = "Dashner"
                },
                new Authors
                {
                    Id = 37, Name = "Markus", Surname = "Zusak"
                },
                new Authors
                {
                    Id = 38, Name = "Amy",Surname = "Schumer"
                },
                new Authors
                {
                    Id = 39, Name = "Madeline",Surname = "Miller"
                },
                new Authors
                {
                    Id = 40, Name = "Colson", Surname = "Whitehead"
                },
                new Authors
                {
                    Id = 41, Name = "G. Norman", Surname = "Lippert"
                },
                new Authors
                {
                    Id = 42, Name = "Chloe", Surname = "Benjamin"
                },
                new Authors
                {
                    Id = 43, Name = "Celeste", Surname = "Ng"
                },
                new Authors
                {
                    Id = 44, Name = "Gail",Surname = "Honeyman"
                },
                new Authors
                {
                    Id = 45, Name = "Kristin",Surname = "Hannah"
                },
                new Authors
                {
                    Id = 46, Name = "Sally", Surname = "Rooney"
                },
                new Authors
                {
                    Id = 47, Name = "Liane", Surname = "Moriarty"
                },
                new Authors
                {
                    Id = 48, Name = "Donna", Surname = "Tartt"
                },
                new Authors
                {
                    Id = 49, Name = "Margaret", Surname = "Atwood"
                },
                new Authors
                {
                    Id = 50, Name = "Kevin", Surname = "Kwan"
                },
                new Authors
                {
                    Id = 51, Name = "Hanya",Surname = "Yanagihara"
                },
                new Authors
                {
                    Id = 52, Name = "A.S.A.", Surname = "Harrison"
                },
                new Authors
                {
                    Id = 53, Name = "M.R.", Surname = "Carey"
                },
                new Authors
                {
                    Id = 54, Name = "Erin", Surname = "Morgenstern"
                },
                new Authors
                {
                    Id = 55, Name = "Anthony", Surname = "Doerr"
                },
                new Authors
                {
                    Id = 56, Name = "Graeme", Surname = "Simsion"
                },
                new Authors
                {
                    Id = 57, Name = "Andy",Surname = "Weir"
                },
                new Authors
                {
                    Id = 58,Name = "Emily",Surname = "St. John Mandel"
                },
                new Authors
                {
                    Id = 59, Name = "Neil",Surname = "Gaiman"
                },
                new Authors
                {
                    Id = 60, Name = "Ernest", Surname = "Cline"
                },
                new Authors
                {
                    Id = 61, Name = "Rainbow", Surname = "Rowell"
                },
                new Authors
                {
                    Id = 62, Name = "John", Surname = "Connolly"
                },
                new Authors
                {
                    Id = 63, Name = "Rebecca",Surname = "Skloot"
                },
                new Authors
                {
                    Id = 64, Name = "M.L.", Surname = "Stedman"
                },
                new Authors
                {
                    Id = 65, Name = "Renée", Surname = "Carlino"
                },
                new Authors
                {
                    Id = 66, Name = "Angie", Surname = "Thomas"
                },
                new Authors
                {
                    Id = 67, Name = "A.J.", Surname = "Finn"
                },
                new Authors
                {
                    Id = 68, Name = "Tayari", Surname = "Jones"
                },
                new Authors
                {
                    Id = 69, Name = "Min", Surname = "Jin Lee"
                },
                new Authors
                {
                    Id = 70, Name = "Glendy", Surname = "Vanderah"
                },
                new Authors
                {
                    Id = 71, Name = "Ann", Surname = "Patchett"
                },
                new Authors
                {
                    Id = 72, Name = "Aldous", Surname = "Huxley"
                },
                new Authors
                {
                    Id = 73, Name = "Dante", Surname = "Alighieri"
                },
                new Authors
                {
                    Id = 74, Name = "Victor", Surname = "Hugo"
                }
            });

        }



        public static void SeedBookAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor[]
            {
                new BookAuthor() {BookId = 1, AuthorId = 1},
                new BookAuthor() {BookId = 2, AuthorId = 2},
                new BookAuthor() {BookId = 3, AuthorId = 3},
                new BookAuthor() {BookId = 4, AuthorId = 4},
                new BookAuthor() {BookId = 5, AuthorId = 5},
                new BookAuthor() {BookId = 6, AuthorId = 6},
                new BookAuthor() {BookId = 7, AuthorId = 7},
                new BookAuthor() {BookId = 8, AuthorId = 8},
                new BookAuthor() {BookId = 9, AuthorId = 9},
                new BookAuthor() {BookId = 10, AuthorId = 10},
                new BookAuthor() {BookId = 11, AuthorId = 11},
                new BookAuthor() {BookId = 12, AuthorId = 12},
                new BookAuthor() {BookId = 13, AuthorId = 6},
                new BookAuthor() {BookId = 14, AuthorId = 13},
                new BookAuthor() {BookId = 15, AuthorId = 14},
                new BookAuthor() {BookId = 16, AuthorId = 15},
                new BookAuthor() {BookId = 17, AuthorId = 16},
                new BookAuthor() {BookId = 18, AuthorId = 17},
                new BookAuthor() {BookId = 19, AuthorId = 18},
                new BookAuthor() {BookId = 20, AuthorId = 8},
                new BookAuthor() {BookId = 21, AuthorId = 12},
                new BookAuthor() {BookId = 22, AuthorId = 19},
                new BookAuthor() {BookId = 23, AuthorId = 20},
                new BookAuthor() {BookId = 24, AuthorId = 21},
                new BookAuthor() {BookId = 25, AuthorId = 22},
                new BookAuthor() {BookId = 26, AuthorId = 23},
                new BookAuthor() {BookId = 27, AuthorId = 24},
                new BookAuthor() {BookId = 28, AuthorId = 25},
                new BookAuthor() {BookId = 29, AuthorId = 26},
                new BookAuthor() {BookId = 30, AuthorId = 27},
                new BookAuthor() {BookId = 31, AuthorId = 28},
                new BookAuthor() {BookId = 32, AuthorId = 29},
                new BookAuthor() {BookId = 33, AuthorId = 30},
                new BookAuthor() {BookId = 34, AuthorId = 31},
                new BookAuthor() {BookId = 35, AuthorId = 32},
                new BookAuthor() {BookId = 36, AuthorId = 33},
                new BookAuthor() {BookId = 37, AuthorId = 34},
                new BookAuthor() {BookId = 38, AuthorId = 35},
                new BookAuthor() {BookId = 39, AuthorId = 36},
                new BookAuthor() {BookId = 40, AuthorId = 37},
                new BookAuthor() {BookId = 41, AuthorId = 38},
                new BookAuthor() {BookId = 42, AuthorId = 39},
                new BookAuthor() {BookId = 43, AuthorId = 40},
                new BookAuthor() {BookId = 44, AuthorId = 41},
                new BookAuthor() {BookId = 45, AuthorId = 42},
                new BookAuthor() {BookId = 46, AuthorId = 43},
                new BookAuthor() {BookId = 47, AuthorId = 44},
                new BookAuthor() {BookId = 48, AuthorId = 45},
                new BookAuthor() {BookId = 49, AuthorId = 46},
                //new BookAuthor() {BookId = 50, AuthorId = 47},
                //new BookAuthor() {BookId = 51, AuthorId = 48},
                //new BookAuthor() {BookId = 52, AuthorId = 49},
                //new BookAuthor() {BookId = 53, AuthorId = 50},
                //new BookAuthor() {BookId = 54, AuthorId = 51},
                //new BookAuthor() {BookId = 55, AuthorId = 52},
                //new BookAuthor() {BookId = 56, AuthorId = 53},
                //new BookAuthor() {BookId = 57, AuthorId = 54},
                //new BookAuthor() {BookId = 58, AuthorId = 55},
                //new BookAuthor() {BookId = 59, AuthorId = 56},
                //new BookAuthor() {BookId = 60, AuthorId = 57},
                //new BookAuthor() {BookId = 61, AuthorId = 58},
                //new BookAuthor() {BookId = 62, AuthorId = 59},
                //new BookAuthor() {BookId = 63, AuthorId = 60},
                //new BookAuthor() {BookId = 64, AuthorId = 61},
                //new BookAuthor() {BookId = 65, AuthorId = 62},
                //new BookAuthor() {BookId = 66, AuthorId = 63},
                //new BookAuthor() {BookId = 67, AuthorId = 64},
                //new BookAuthor() {BookId = 68, AuthorId = 65},
                //new BookAuthor() {BookId = 69, AuthorId = 45},
                //new BookAuthor() {BookId = 70, AuthorId = 66},
                //new BookAuthor() {BookId = 71, AuthorId = 67},
                //new BookAuthor() {BookId = 72, AuthorId = 68},
                //new BookAuthor() {BookId = 73, AuthorId = 69},
                //new BookAuthor() {BookId = 74, AuthorId = 49},
                //new BookAuthor() {BookId = 75, AuthorId = 70},
                //new BookAuthor() {BookId = 76, AuthorId = 71},
                //new BookAuthor() {BookId = 77, AuthorId = 7},
                //new BookAuthor() {BookId = 78, AuthorId = 72},
                //new BookAuthor() {BookId = 79, AuthorId = 73},
                //new BookAuthor() {BookId = 80, AuthorId = 74},
            });
        }

        public static void SeedBookGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>().HasData(new BookGenre[]
            {
                new BookGenre() {BookId = 1, GenreId = 1},
                new BookGenre() {BookId = 2, GenreId = 2},
                new BookGenre() {BookId = 3, GenreId = 3},
                new BookGenre() {BookId = 4, GenreId = 3},
                new BookGenre() {BookId = 5, GenreId = 4},
                new BookGenre() {BookId = 6, GenreId = 2},
                new BookGenre() {BookId = 7, GenreId = 5},
                new BookGenre() {BookId = 8, GenreId = 1},
                new BookGenre() {BookId = 9, GenreId = 6},
                new BookGenre() {BookId = 10, GenreId = 1},
                new BookGenre() {BookId = 11, GenreId = 7},
                new BookGenre() {BookId = 12, GenreId = 4},
                new BookGenre() {BookId = 13, GenreId = 2},
                new BookGenre() {BookId = 14, GenreId = 7},
                new BookGenre() {BookId = 15, GenreId = 4},
                new BookGenre() {BookId = 16, GenreId = 8},
                new BookGenre() {BookId = 17, GenreId = 1},
                new BookGenre() {BookId = 18, GenreId = 2},
                new BookGenre() {BookId = 19, GenreId = 2},
                new BookGenre() {BookId = 20, GenreId = 1},
                new BookGenre() {BookId = 21, GenreId = 4},
                new BookGenre() {BookId = 22, GenreId = 10},
                new BookGenre() {BookId = 23, GenreId = 7},
                new BookGenre() {BookId = 24, GenreId = 1},
                new BookGenre() {BookId = 25, GenreId = 10},
                new BookGenre() {BookId = 26, GenreId = 11},
                new BookGenre() {BookId = 27, GenreId = 11},
                new BookGenre() {BookId = 28, GenreId = 12},
                new BookGenre() {BookId = 29, GenreId = 3},
                new BookGenre() {BookId = 30, GenreId = 13},
                new BookGenre() {BookId = 31, GenreId = 14},
                new BookGenre() {BookId = 32, GenreId = 3},
                new BookGenre() {BookId = 33, GenreId = 15},
                new BookGenre() {BookId = 34, GenreId = 15},
                new BookGenre() {BookId = 35, GenreId = 13},
                new BookGenre() {BookId = 36, GenreId = 2},
                new BookGenre() {BookId = 37, GenreId = 13},
                new BookGenre() {BookId = 38, GenreId = 16},
                new BookGenre() {BookId = 39, GenreId = 15},
                new BookGenre() {BookId = 40, GenreId = 3},
                new BookGenre() {BookId = 41, GenreId = 11},
                new BookGenre() {BookId = 42, GenreId = 2},
                new BookGenre() {BookId = 43, GenreId = 13},
                new BookGenre() {BookId = 44, GenreId = 3},
                new BookGenre() {BookId = 45, GenreId = 17},
                new BookGenre() {BookId = 46, GenreId = 18},
                new BookGenre() {BookId = 47, GenreId = 18},
                new BookGenre() {BookId = 48, GenreId = 13},
                new BookGenre() {BookId = 49, GenreId = 4},
                //new BookGenre() {BookId = 50, GenreId = 3},
                //new BookGenre() {BookId = 51, GenreId = 17},
                //new BookGenre() {BookId = 52, GenreId = 19},
                //new BookGenre() {BookId = 53, GenreId = 4},
                //new BookGenre() {BookId = 54, GenreId = 18},
                //new BookGenre() {BookId = 55, GenreId = 20},
                //new BookGenre() {BookId = 56, GenreId = 5},
                //new BookGenre() {BookId = 57, GenreId = 2},
                //new BookGenre() {BookId = 58, GenreId = 13},
                //new BookGenre() {BookId = 59, GenreId = 21},
                //new BookGenre() {BookId = 60, GenreId = 5},
                //new BookGenre() {BookId = 61, GenreId = 5},
                //new BookGenre() {BookId = 62, GenreId = 2},
                //new BookGenre() {BookId = 63, GenreId = 5},
                //new BookGenre() {BookId = 64, GenreId = 15},
                //new BookGenre() {BookId = 65, GenreId = 2},
                //new BookGenre() {BookId = 66, GenreId = 22},
                //new BookGenre() {BookId = 67, GenreId = 13},
                //new BookGenre() {BookId = 68, GenreId = 18},
                //new BookGenre() {BookId = 69, GenreId = 23},
                //new BookGenre() {BookId = 70, GenreId = 15},
                //new BookGenre() {BookId = 71, GenreId = 14},
                //new BookGenre() {BookId = 72, GenreId = 18},
                //new BookGenre() {BookId = 73, GenreId = 13},
                //new BookGenre() {BookId = 74, GenreId = 19},
                //new BookGenre() {BookId = 75, GenreId = 18},
                //new BookGenre() {BookId = 76, GenreId = 18},
                //new BookGenre() {BookId = 77, GenreId = 19},
                //new BookGenre() {BookId = 78, GenreId = 19},
                //new BookGenre() {BookId = 79, GenreId = 1},
                //new BookGenre() {BookId = 80, GenreId = 13},
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
                    PhoneNumber = "+38012345678",
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
                    Email = "john.doe@example.com",
                    PhoneNumber = "+380111111111",
                },
                new Client
                {
                    CredentialsId = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+38022222222",
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
                new Photo() {Id = 1, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=1 },
                new Photo() {Id = 2, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=2 },
                new Photo() {Id = 3, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=3 },
                new Photo() {Id = 4, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=4 },
                new Photo() {Id = 5, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=5 },
                new Photo() {Id = 6, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=6 },
                new Photo() {Id = 7, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=7 },
                new Photo() {Id = 8, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=8 },
                new Photo() {Id = 9, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=9 },
                new Photo() {Id = 10, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=10 },
                new Photo() {Id = 11, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=11 },
                new Photo() {Id = 12, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=12 },
                new Photo() {Id = 13, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=13 },
                new Photo() {Id = 14, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=14 },
                new Photo() {Id = 15, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=15 },
                new Photo() {Id = 16, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=16 },
                new Photo() {Id = 17, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=17 },
                new Photo() {Id = 18, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=18 },
                new Photo() {Id = 19, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=19 },
                new Photo() {Id = 20, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=20 },
                new Photo() {Id = 21, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=21 },
                new Photo() {Id = 22, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=22 },
                new Photo() {Id = 23, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=23 },
                new Photo() {Id = 24, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=24 },
                new Photo() {Id = 25, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=25 },
                new Photo() {Id = 26, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=26 },
                new Photo() {Id = 27, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=27 },
                new Photo() {Id = 28, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=28 },
                new Photo() {Id = 29, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=29 },
                new Photo() {Id = 30, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=30 },
                new Photo() {Id = 31, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=31 },
                new Photo() {Id = 32, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=32 },
                new Photo() {Id = 33, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=33 },
                new Photo() {Id = 34, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=34 },
                new Photo() {Id = 35, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=35 },
                new Photo() {Id = 36, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=36 },
                new Photo() {Id = 37, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=37 },
                new Photo() {Id = 38, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=38 },
                new Photo() {Id = 39, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=39 },
                new Photo() {Id = 40, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=40 },
                new Photo() {Id = 41, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=41 },
                new Photo() {Id = 42, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=42 },
                new Photo() {Id = 43, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=43 },
                new Photo() {Id = 44, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=44 },
                new Photo() {Id = 45, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=45 },
                new Photo() {Id = 46, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=46 },
                new Photo() {Id = 47, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=47 },
                new Photo() {Id = 48, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=48 },
                new Photo() {Id = 49, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=49 },
                new Photo() {Id = 50, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=50 },
                //new Photo() {Id = 51, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=51 },
                //new Photo() {Id = 52, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=52 },
                //new Photo() {Id = 53, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=53 },
                //new Photo() {Id = 54, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=54 },
                //new Photo() {Id = 55, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=55 },
                //new Photo() {Id = 56, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=56 },
                //new Photo() {Id = 57, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=57 },
                //new Photo() {Id = 58, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=58 },
                //new Photo() {Id = 59, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=59 },
                //new Photo() {Id = 60, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=60 },
                //new Photo() {Id = 61, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=61 },
                //new Photo() {Id = 62, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=62 },
                //new Photo() {Id = 63, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=63 },
                //new Photo() {Id = 64, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=64 },
                //new Photo() {Id = 65, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=65 },
                //new Photo() {Id = 66, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=66 },
                //new Photo() {Id = 67, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=67 },
                //new Photo() {Id = 68, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=68 },
                //new Photo() {Id = 69, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=69 },
                //new Photo() {Id = 70, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=70 },
                //new Photo() {Id = 71, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=71 },
                //new Photo() {Id = 72, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=72 },
                //new Photo() {Id = 73, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=73 },
                //new Photo() {Id = 74, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=74 },
                //new Photo() {Id = 75, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=75 },
                //new Photo() {Id = 76, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=76 },
                //new Photo() {Id = 77, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=77 },
                //new Photo() {Id = 78, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=78 },
                //new Photo() {Id = 79, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=79 },
                //new Photo() {Id = 80, Name = Path.GetFileName("Image\\genericBookCover.jpg"), ImageData=File.ReadAllBytes("Image\\genericBookCover.jpg"), BookId=80 },
            });
        }
    }
}
