using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;

namespace WebApplication6.Data
{
    
    public static class BooksFilter
    {     
        public static string CurrentAuthor { get; set; }  
        public static Genre? CurrentGenre { get; set; }   
        public static BookCondition? CurrentBookCondition { get; set; }    

        public static async Task<PaginationList<Book>> GetFilteredBooks(IQueryable<Book> books, BooksFilterArgs filterArgs, int? catalogPage) 
        {
          
            books = GetFilteredBooksBySearchString(books, filterArgs.SearchString, ref catalogPage);

            books = GetFilteredBooksByGenre(books, filterArgs.Genre);

            books = GetFilteredBooksByAuthor(books, filterArgs.Author);

            books = SwitchBookCondition(books, filterArgs.BookCondition);

            SetCurrentBookFilters(filterArgs);

            return await PaginationList<Book>.CreateAsync(books.AsNoTracking(), catalogPage?? 1, pageSize: 3);
        }

        public static IQueryable<Book> SwitchBookCondition(IQueryable<Book> books, BookCondition? bookCondition)
        {           
            if(bookCondition.HasValue)
            {
                switch (bookCondition)
                {
                    case BookCondition.Used:
                        books = books.Where(b => b.IsSecondHand == true);
                        break;
                    case BookCondition.New:
                        books = books.Where(b => b.IsSecondHand == false);
                        break;
                }
            }              
           
            return books;
        }
        public static IQueryable<Book> GetFilteredBooksByGenre(IQueryable<Book> books, Genre? genre)
        {
            if(genre.HasValue)
               books = books.Where(b => b.Genre == genre);

            return books;
        }
        public static IQueryable<Book> GetFilteredBooksBySearchString(IQueryable<Book> books, string searchString, ref int? catalogPage)
        {
            if (searchString != null)
            {
                catalogPage = 1;
                CurrentAuthor = null;
                CurrentBookCondition = null;
                CurrentGenre = null;
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.LastName.Contains(searchString));            
            }

            return books;
        }
        public static IQueryable<Book> GetFilteredBooksByAuthor(IQueryable<Book> books, string author)
        {
            if (author != null)
                books = books.Where(b => b.Author.LastName == author);

            return books;
        }
        public static void SetCurrentBookFilters(BooksFilterArgs filterArgs)
        {
            CurrentAuthor = filterArgs.Author;
            CurrentGenre = filterArgs.Genre;
            CurrentBookCondition = filterArgs.BookCondition;
        }
        


    }
}
