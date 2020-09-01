using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class BooksFilterArgs
    {
        public string SearchString { get; set; }
        public  Genre? Genre { get; set; }
        public BookCondition? BookCondition { get; set; }
        public string Author { get; set; }

        public BooksFilterArgs(string searchString, string author, Genre? genre, BookCondition? bookCondition) =>
            (SearchString, Author, Genre, BookCondition) = (searchString, author, genre, bookCondition);
    }
}
