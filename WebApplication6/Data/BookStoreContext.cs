using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication6.Data.Models;

namespace WebApplication6.Models
{
    public class BookStoreContext : IdentityDbContext<User>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
        public DbSet<OrderDetailes> OrderDetailes { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
}
