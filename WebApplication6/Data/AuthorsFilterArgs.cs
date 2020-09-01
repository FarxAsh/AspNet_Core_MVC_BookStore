using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class AuthorsFilterArgs
    {
        public string SearchString { get; set; }
        public Genre? Genre { get; set; }
        public string SortOrder { get; set; }

        public AuthorsFilterArgs(string searchString, string sortOrder,  Genre? genre) =>
            (SearchString, SortOrder, Genre) = (searchString, sortOrder, genre);
    }
}
