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

        public static async Task<PaginationList<Book>> GetFilteredBooks(BookStoreContext context, BooksFilterArgs filterArgs, int? catalogPage) 
        {
            var books = from b in context.Book.Include(b => b.Author)
                        select b;

            CurrentAuthor = filterArgs.Author;
            CurrentGenre =  filterArgs.Genre;
            CurrentBookCondition =  filterArgs.BookCondition;        

            if (!string.IsNullOrEmpty(filterArgs.SearchString))
            {
                catalogPage = 1;
                CurrentAuthor = null;
                CurrentBookCondition = null;
                CurrentGenre = null;
                books = books.Where(b => b.Title.Contains(filterArgs.SearchString) || b.Author.LastName.Contains(filterArgs.SearchString));
                return await PaginationList<Book>.CreateAsync(books.AsNoTracking(), catalogPage?? 1, pageSize: 3);
            }

            if (!string.IsNullOrEmpty(filterArgs.Genre.ToString()))
                catalogPage = 1;
            else
                filterArgs.Genre = CurrentGenre;

            if (!string.IsNullOrEmpty(filterArgs.Author))
                catalogPage = 1;
            else
                filterArgs.Author = CurrentAuthor;

            if (!string.IsNullOrEmpty(filterArgs.BookCondition.ToString()))
                catalogPage = 1;
            else
                filterArgs.BookCondition = CurrentBookCondition;

            if (!string.IsNullOrEmpty(filterArgs.Author))
                books = books.Where(b => b.Author.LastName == filterArgs.Author);

            books = SwitchBookCondition(books, filterArgs.BookCondition);
            books = SwitchBookGenre(books, filterArgs.Genre);               

            return await PaginationList<Book>.CreateAsync(books.AsNoTracking(), catalogPage?? 1, pageSize: 3);
        }

        public static IQueryable<Book> SwitchBookCondition(IQueryable<Book> books, BookCondition? bookCondition)
        {
            if (!string.IsNullOrEmpty(bookCondition.ToString()))
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
        public static IQueryable<Book> SwitchBookGenre(IQueryable<Book> books,Genre? genre)
        {
            if (!string.IsNullOrEmpty(genre.ToString()))
            {
                switch (genre)
                {
                    case Genre.Fiction:

                        books = books.Where(b => b.Genre == genre);

                        break;
                    case Genre.Fantasy:

                        books = books.Where(b => b.Genre == genre);

                        break;
                    case Genre.Buisness:

                        books = books.Where(b => b.Genre == genre);

                        break;
                    case Genre.History:

                        books = books.Where(b => b.Genre == genre);

                        break;
                    case Genre.Horror:

                        books = books.Where(b => b.Genre == genre);

                        break;
                    case Genre.Science:

                        books = books.Where(b => b.Genre == genre);

                        break;
                }
            }

            return books;

        }


    }
}
