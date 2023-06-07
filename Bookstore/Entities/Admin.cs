using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Entities
{
    public class Admin
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        [MaxLength(13)]
        [Required]
        public string PhoneNumber { get; set; }

        public bool Status_Admin { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
