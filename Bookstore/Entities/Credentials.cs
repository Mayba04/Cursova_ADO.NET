using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Credentials
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int? ClientId { get; set; }

        public Client Client { get; set; }
    }
}
