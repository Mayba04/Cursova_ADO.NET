using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<OrderBook> OrderBooks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Photo CoverPhoto { get; set; }
    }
}
