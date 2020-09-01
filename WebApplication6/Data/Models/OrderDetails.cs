using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public class OrderDetailes
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int Price { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
