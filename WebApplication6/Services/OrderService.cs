using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Services
{
   
    public class OrderService: IOrderService
    {
        private readonly BookStoreContext _context;
        private readonly ShopBasket _basket;

        public OrderService(BookStoreContext context, ShopBasket basket)
        {
            _context = context;
            _basket = basket;
        }

        public async void createOrder(Order order)
        {
            foreach (var item in await _basket.GetBasketItemsAsync())
            {
                order.TotalPrice += item.Book.Price;

            }
            _context.Order.Add(order);
            _context.SaveChanges();
         
            foreach (var item in  await _basket.GetBasketItemsAsync())
            {
                _context.OrderDetailes.Add(new OrderDetailes
                {
                    OrderID = order.ID,
                    BookID = item.Book.ID,
                    Price = item.Book.Price
                });
                
            }
            _context.SaveChanges();
        }
    }
}
