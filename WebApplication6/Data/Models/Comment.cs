using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
        public int BookID { get; set; }
        public string UserName { get; set; }
        public Book Book { get; set; }
    }
}
