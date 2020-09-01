using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
   
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext context)
        { 
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Author.ToList());
        }

        public IActionResult AboutUs()
        {

            return View();
        }

        public IActionResult ContactUs()
        {

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View(await _context.Author.FirstOrDefaultAsync(a => a.ID == id));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
