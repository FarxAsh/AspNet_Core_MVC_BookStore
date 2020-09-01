using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public class BasketItem
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public string myBasket { get; set; }
    }
}
