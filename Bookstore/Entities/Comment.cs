using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
