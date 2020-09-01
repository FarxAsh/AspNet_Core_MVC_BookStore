using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class AuthorsFilter
    {
        public static string CurrentSortOrder { get; set; }
        public static Genre? CurrentGenre { get; set; }
        public static async Task<PaginationList<Author>> GetFilteredAuthors(BookStoreContext context, AuthorsFilterArgs filterArgs, int? catalogPage)
        {
            var authors = from b in context.Author.Include(b => b.Book)
                          select b;

            CurrentSortOrder = !string.IsNullOrEmpty(filterArgs.SortOrder)? "desc" : "asc";
            CurrentGenre = filterArgs.Genre;

            if (!string.IsNullOrEmpty(filterArgs.SearchString))
            {
                catalogPage = 1;
                CurrentSortOrder = null;
                CurrentGenre = null;
                authors = authors.Where(s => s.LastName.Contains(filterArgs.SearchString));
                return await PaginationList<Author>.CreateAsync(authors.AsNoTracking(), catalogPage ?? 1, pageSize: 3);
            }

            if (!string.IsNullOrEmpty(filterArgs.Genre.ToString()))
                catalogPage = 1;
            else
                filterArgs.Genre = CurrentGenre;

            authors = SelectSortOrder(authors, filterArgs.SortOrder);
            authors = SwitchAuthorGenre(authors, filterArgs.Genre);

            return await PaginationList<Author>.CreateAsync(authors.AsNoTracking(), catalogPage ?? 1, pageSize: 3);
        }

        
        public static IQueryable<Author> SwitchAuthorGenre(IQueryable<Author> authors, Genre? genre)
        {
            if (!string.IsNullOrEmpty(genre.ToString()))
            {
                switch (genre)
                {
                    case Genre.Fiction:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                    case Genre.Fantasy:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                    case Genre.Buisness:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                    case Genre.History:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                    case Genre.Horror:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                    case Genre.Science:

                        authors = authors.Where(b => b.Genre == genre);

                        break;
                }
            }

            return authors;

        }
        public static IQueryable<Author> SelectSortOrder(IQueryable<Author> authors, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "desc":
                        authors = authors.OrderByDescending(a => a.LastName);
                        CurrentSortOrder = null;
                        break;

                    case "asc":
                        authors = authors.OrderBy(a => a.LastName);
                        break;
                }
            }
            
           return authors;

        }
    }
}
