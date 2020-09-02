using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Services
{
    public class AuthorFilteringService
    {
        public static string CurrentSortOrder { get; private set; }
        public async Task<PaginationList<Author>> GetFilteredAuthors(BookStoreContext context, AuthorFilterArgs filterArgs, int? catalogPage)
        {

            CurrentSortOrder = !string.IsNullOrEmpty(filterArgs.SortOrder) ? "desc" : "asc";

            var authors = context.Author.Include(author => author.Book).Select(author => author);           

            authors = GetFilteredAuthorsByGenre(authors, filterArgs.Genre);               
            
            authors = GetFilteredAuthorsBySearchString(authors, filterArgs.SearchString, ref catalogPage);

            authors = SelectSortOrder(authors, filterArgs.SortOrder);       
         
            return await PaginationList<Author>.CreateAsync(authors.AsNoTracking(), catalogPage ?? 1, pageSize: 3);
        }            
        IQueryable<Author> SelectSortOrder(IQueryable<Author> authors, string sortOrder)
        {
                switch (sortOrder)
                {
                    case "desc":
                        authors = authors.OrderByDescending(author => author.LastName);
                        CurrentSortOrder = string.Empty;
                        break;

                    case "asc":
                        authors = authors.OrderBy(author => author.LastName);
                        break;
                }
            return authors;
        }
        IQueryable<Author> GetFilteredAuthorsByGenre(IQueryable<Author> authors, Genre? genre)
        {
            if (genre.HasValue)
                authors = authors.Where(author => author.Genre == genre);

            return authors;
        }   
        IQueryable<Author> GetFilteredAuthorsBySearchString(IQueryable<Author> authors, string searchString, ref int? catalogPage)
        {
            if (searchString != null)
            {
                catalogPage = 1;
                authors = authors.Where(author => author.LastName.Contains(searchString));
            }

            return authors;
        }   
        
    }
}
