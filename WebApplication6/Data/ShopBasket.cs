using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public class ShopBasket
    {
        BookStoreContext _context;
        public ShopBasket(BookStoreContext context) => 
            _context = context; 

        public string basketID { get; set; }
        public ICollection<BasketItem> defferedBooks { get; set; }

        public int totalSum = 0;

        public static ShopBasket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetRequiredService<BookStoreContext>();
            string _basketID = session.GetString("basketID") ?? Guid.NewGuid().ToString();

            session.SetString("basketID", _basketID);

            return new ShopBasket(context) { basketID = _basketID };
        }

        public async Task AddBasketItemAsync(Book book)
        {
            await _context.BasketItem.AddAsync(new BasketItem
            {
                Book = book,
                myBasket = basketID
            });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBasketItemAsync(int Id)
        {
            var item = _context.BasketItem.FirstOrDefault(i => i.ID == Id);

            if(item != null)
            {
                _context.BasketItem.Remove(item);
               await _context.SaveChangesAsync();
            }
            
        }

        public async Task<ICollection<BasketItem>> GetBasketItemsAsync()
        {
            return (await _context.BasketItem.Where(bi => bi.myBasket == basketID).Include(b => b.Book).ThenInclude(a => a.Author).ToListAsync());
        }

        public async Task RemoveAllBasketItems()
        {
            var items = await _context.BasketItem.Where(bi => bi.myBasket == basketID).ToListAsync();
            
            if(items != null)
            {
                _context.BasketItem.RemoveRange(items);
               await _context.SaveChangesAsync();
            }

        }

        public async Task<int> GetTotalSumAsync()
        {
            var items = await _context.BasketItem.Where(b => b.myBasket == basketID).Include(b => b.Book).AsNoTracking().ToListAsync();

            foreach (var item in  items)
            {
                totalSum += item.Book.Price;
            }

            return totalSum;   
        }


    }
}
