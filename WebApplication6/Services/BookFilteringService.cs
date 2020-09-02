using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;

namespace WebApplication6.Services
{   
    public class BookFilteringService
    {                  
        public async Task<PaginationList<Book>> GetFilteredBooks(BookStoreContext context, BookFilterArgs filterArgs, int? catalogPage) 
        {

            var books = context.Book.Include(book => book.Author).Select(book => book);
                      

            books = GetFilteredBooksBySearchString(books, filterArgs.SearchString, ref catalogPage);
         
            books = GetFilteredBooksByGenre(books, filterArgs.Genre);

            books = GetFilteredBooksByAuthor(books, filterArgs.Author);

            books = SwitchBookCondition(books, filterArgs.BookCondition);        

            return await PaginationList<Book>.CreateAsync(books.AsNoTracking(), catalogPage?? 1, pageSize: 3);
        }

        public IQueryable<Book> SwitchBookCondition(IQueryable<Book> books, BookCondition? bookCondition)
        {           
            if(bookCondition.HasValue)
            {
                switch (bookCondition)
                {
                    case BookCondition.Used:
                        books = books.Where(book => book.IsSecondHand == true);
                        break;
                    case BookCondition.New:
                        books = books.Where(book => book.IsSecondHand == false);
                        break;
                }
            }              
           
            return books;
        }
        public IQueryable<Book> GetFilteredBooksByGenre(IQueryable<Book> books, Genre? genre)
        {
            if(genre.HasValue)
               books = books.Where(book => book.Genre == genre);

            return books;
        }
        public IQueryable<Book> GetFilteredBooksBySearchString(IQueryable<Book> books, string searchString, ref int? catalogPage)
        {
            if (searchString != null)
            {
                catalogPage = 1;
                books = books.Where(book => book.Title.Contains(searchString) || book.Author.LastName.Contains(searchString));            
            }

            return books;
        }
        public IQueryable<Book> GetFilteredBooksByAuthor(IQueryable<Book> books, string author)
        {
            if (author != null)
                books = books.Where(book => book.Author.LastName == author);

            return books;
        }
             
    }
}
