using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class BookFilterArgs
    {
        public string SearchString { get; set; }
        public  Genre? Genre { get; set; }
        public BookCondition? BookCondition { get; set; }
        public string Author { get; set; }
        public BookFilterArgs() { }
    }
}
