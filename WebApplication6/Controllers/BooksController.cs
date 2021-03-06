﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication6.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using WebApplication6.Controllers;
using WebApplication6.Data.ViewModels;
using WebApplication6.Data.Models;
using WebApplication6.Data;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{

    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext _context;
        private readonly ShopBasket _basket;
        private readonly BookFilteringService _bookFilteringService;
        public BooksController(ILogger<HomeController> logger, BookStoreContext context, ShopBasket basket, BookFilteringService bookFilteringService ) =>
            (_logger, _context, _basket, _bookFilteringService) = (logger, context, basket, bookFilteringService);
                          
        public async Task<IActionResult> GetAllBooks()
        {
                    
            var books = await PaginationList<Book>.CreateAsync(_context.Book.Include(book => book.Author), pageIndex:1, pageSize: 3);

            var authors = await _context.Author.AsNoTracking().ToListAsync();

            return View(new GetAllBooksViewModel() {Books = books, Authors = authors });
        }

        public async Task<IActionResult> GetAllBooksAjax(BookFilterArgs filterArgs, int? catalogPage)
        {
            
            var books = await _bookFilteringService.GetFilteredBooks(_context, filterArgs, catalogPage);
            return Json(books);
        }

        [HttpGet]   
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Create()
        {      
            var authors = await _context.Author.AsNoTracking().ToListAsync();
            CreateBookViewModel vm = new CreateBookViewModel() { Author = authors };            
            return View(vm);
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Year,Pages,Image,Description,Genre,Price,AuthorID")] Book book)
        {             
            var authors = await _context.Author.AsNoTracking().ToListAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    if(User.IsInRole("user"))
                    {
                        book.IsSecondHand = true;
                    }
                    else
                    {
                        book.IsSecondHand = false;
                    }
                    _context.Book.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetAllBooks));
                }
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError(ex, "An error ocured updating DB");
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(new CreateBookViewModel { Book=book,Author=authors});
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
         
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var authors = await _context.Author.AsNoTracking().ToListAsync();
            EditBookViewModel vm = new EditBookViewModel() { Author = authors,Book=book };

            return View(vm);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Year,Pages,Image,Description,Genre,Price,AuthorID")] Book book)
        {
            var authors = await _context.Author.AsNoTracking().ToListAsync();
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Book.Update(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetAllBooks));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "An error occurred updating DB");
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(new EditBookViewModel {Book=book,Author=authors });
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(b => b.Author)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(book);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(GetAllBooks));
            }

            try
            {

                var items = await _context.BasketItem.Where(i => i.Book.ID == id).ToListAsync();
                if(items.Any())
                {
                    _context.BasketItem.RemoveRange(items);
                    await _context.SaveChangesAsync();
                }
               
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllBooks));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred updating DB");
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var book = await _context.Book.Include(b => b.Author).Include(b => b.Comment).FirstOrDefaultAsync(b => b.ID == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> addFeedBack(string feedback, int id)
        {
            Comment comment = new Comment { BookID = id, Feedback = feedback, UserName = User.Identity.Name, Date = DateTime.Now };
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Books", new {id=id});
        }
    }
}
