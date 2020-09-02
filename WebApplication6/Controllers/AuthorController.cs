using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication6.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication6.Data;
using WebApplication6.Data.ViewModels;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext _context;
        private readonly AuthorFilteringService _authorFilteringService;
        public AuthorController(ILogger<HomeController> logger, BookStoreContext context, AuthorFilteringService authorFilteringService)
        {
            _logger = logger;
            _context = context;
            _authorFilteringService = authorFilteringService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors(AuthorFilterArgs filterArgs, int? catalogPage)
        {       
            var authors = await _authorFilteringService.GetFilteredAuthors(_context, filterArgs, catalogPage);

            return View(new GetAuthorsViewModel() {Authors = authors, FilterArgs = filterArgs });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
            
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Author newAuthor)
        {
          
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Author.AddAsync(newAuthor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetAuthors));
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occured updating DB");
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(newAuthor);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
       

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Image,ShortBiography,Biography,Genre")] Author author)
        {    

            if (id != author.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Author.Update(author);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetAuthors));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "An error occured updating DB");
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(author);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Author.Include(b => b.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ID == id);
            if (authors == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(authors);
        }


        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.Include(a => a.Book).FirstOrDefaultAsync(a => a.ID == id);
            if (author == null)
            {
                return RedirectToAction(nameof(GetAuthors));
            }

            try
            {

                foreach(var book in author.Book)
                {
                    if(book != null)
                    {
                        var items = await _context.BasketItem.Where(i => i.Book.ID == book.ID).ToListAsync();
                        if (items.Any())
                        {
                            _context.BasketItem.RemoveRange(items);
                            await _context.SaveChangesAsync();
                        }
                    }
                  
                
                }      
                _context.Author.Remove(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAuthors));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occured updating DB");
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

       
    }



}


