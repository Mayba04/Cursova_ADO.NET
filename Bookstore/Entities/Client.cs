using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Client
    {
        public int CredentialsId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Status_admin { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Credentials Credentials { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
