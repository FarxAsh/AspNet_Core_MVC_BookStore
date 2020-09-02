using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data.ViewModels
{
    public class GetAuthorsViewModel
    {
        public PaginationList<Author> Authors { get; set; }
        public AuthorFilterArgs FilterArgs { get; set; }
    }
}
