using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.ViewModels;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BookStoreContext _context;
       

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, BookStoreContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
          
        }
       


        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetUsersOrders()
        {

            var orders = await _context.Order.Include(o => o.OrderDetailes).Where(o => o.Byer == User.Identity.Name).AsNoTracking().ToListAsync();

            return View(orders);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Details(int id)
        {

            var orderDetails = await _context.OrderDetailes.Where(od => od.OrderID == id).Include(od => od.Book).ThenInclude(b => b.Author).AsNoTracking().ToListAsync();
           

            return View(orderDetails);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
             
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                     return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login or password");
                }
            }
            return View(model);
        }
        
        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
