using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data.ViewModels
{
    public class CreateBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Author { get; set; }
    }
}
