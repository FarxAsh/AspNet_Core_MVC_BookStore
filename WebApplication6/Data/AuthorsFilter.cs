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
        public static async Task<PaginationList<Author>> GetFilteredAuthors(IQueryable<Author> authors, AuthorsFilterArgs filterArgs, int? catalogPage)
        {
           
            CurrentSortOrder = !string.IsNullOrEmpty(filterArgs.SortOrder)? "desc" : "asc";

            authors = GetFilteredAuthorsByGenre(authors, filterArgs.Genre);               
            
            authors = GetFilteredAuthorsBySearchString(authors, filterArgs.SearchString, ref catalogPage);

            authors = SelectSortOrder(authors, filterArgs.SortOrder);

            SetCurrentAuthorFilters(filterArgs);
           
            return await PaginationList<Author>.CreateAsync(authors.AsNoTracking(), catalogPage ?? 1, pageSize: 3);
        }            
        public static IQueryable<Author> SelectSortOrder(IQueryable<Author> authors, string sortOrder)
        {
                switch (sortOrder)
                {
                    case "desc":
                        authors = authors.OrderByDescending(a => a.LastName);
                        CurrentSortOrder = string.Empty;
                        break;

                    case "asc":
                        authors = authors.OrderBy(a => a.LastName);
                        break;
                }
            return authors;
        }
        public static IQueryable<Author> GetFilteredAuthorsByGenre(IQueryable<Author> authors, Genre? genre)
        {
            if (genre.HasValue)
                authors = authors.Where(b => b.Genre == genre);

            return authors;
        }   
        public static IQueryable<Author> GetFilteredAuthorsBySearchString(IQueryable<Author> authors, string searchString, ref int? catalogPage)
        {
            if (searchString != null)
            {
                catalogPage = 1;
                CurrentGenre = null;
                authors = authors.Where(s => s.LastName.Contains(searchString));
            }

            return authors;
        }   
        public static void SetCurrentAuthorFilters(AuthorsFilterArgs filterArgs)
        {
            CurrentGenre = filterArgs.Genre;           
        }
    }
}
