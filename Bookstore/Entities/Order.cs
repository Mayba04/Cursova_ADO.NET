using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool Payment_status { get; set; }
        public Client Clients { get; set; }
        public ICollection<OrderBook> OrderBooks { get; set; }
    }
}
