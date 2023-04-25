using Bookstore;
using Bookstore.Entities;
using Bookstore.Repository;
using System;

namespace Cursova_ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

            IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());

        }
    }
}
