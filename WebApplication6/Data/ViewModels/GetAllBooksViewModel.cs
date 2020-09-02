using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using WebApplication6.Data;

namespace WebApplication6.Data.ViewModels
{
    public class GetAllBooksViewModel
    {
        public List<Author> Authors { get;set; }
        public PaginationList<Book> Books { get; set; }
        public BookFilterArgs FilterArgs { get; set; }
    }
}
