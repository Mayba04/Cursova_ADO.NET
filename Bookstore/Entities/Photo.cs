using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public int BookId { get; set; }  
        public Book Book { get; set; }
    }
}
