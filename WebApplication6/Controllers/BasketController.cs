using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
  
  
    public class BasketController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly ShopBasket _basket;

        public BasketController(ShopBasket basket, BookStoreContext context)
        {
            _context = context;
            _basket = basket;
        }
        [HttpGet]

        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetBasketView()
        {
            var items = await _basket.GetBasketItemsAsync();
            _basket.defferedBooks = items;
            ViewData["TotalSum"] = await _basket.GetTotalSumAsync();
            

            return View(_basket);
        }

        [Authorize(Roles = "user")]
        public  async Task<EmptyResult> AddBasketItem(int id)
        {
            await _basket.AddBasketItemAsync( _context.Book.FirstOrDefault(b => b.ID == id));

            return new EmptyResult();
           
        }


        [Authorize(Roles = "user")]
        public async Task<IActionResult> RemoveBasketItemAsync(int id)
        {
            await _basket.RemoveBasketItemAsync(id);
            
          
            return RedirectToAction(nameof(GetBasketView));
            
           

        }
    }
}

