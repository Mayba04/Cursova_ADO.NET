using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public Client Client { get; set; }
        public Book Book { get; set; }
    }
}
