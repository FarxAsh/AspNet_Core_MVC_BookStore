using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ShopBasket _basket;
        public OrderController(IOrderService orderService, ShopBasket basket)
        {
            _orderService = orderService;
            _basket = basket;
        }

        [Authorize(Roles = "user")]
        public IActionResult Buy()
        {           
            return View();
        }
        
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> Buy(Order order)
        {        
            if(!(await _basket.GetBasketItemsAsync()).Any())
            {
                ModelState.AddModelError("", "DSDSDS");
            }
            if(ModelState.IsValid)
            {
                order.orderDateTime = DateTime.Now;
                order.Byer = User.Identity.Name;
                _orderService.createOrder(order);
                return RedirectToAction(nameof(Complete));
            }       
            return View(order);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Complete()
        {
            await _basket.RemoveAllBasketItems();
            ViewBag.Message = "Order is created!";
            return View();
        }
    }


}