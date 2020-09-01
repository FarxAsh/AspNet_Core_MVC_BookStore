using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.ViewModels;
using WebApplication6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Controllers
{
    
    [Authorize(Roles="admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        BookStoreContext _context;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager,BookStoreContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> RemoveOrderItem(int id)
        {

            var order = await _context.Order.FindAsync(id);
            if(order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }        

            return RedirectToAction(nameof(GetAllUsersOrders));



        }


        public async Task<IActionResult> GetAllUsersOrders()
        {

            var orders = await _context.Order.AsNoTracking().ToListAsync();

            return View(orders);
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        public  async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(UserList));
        }

        public async Task<IActionResult> Details(int id)
        {

            var orderDetails = await _context.OrderDetailes.Where(od => od.OrderID == id).Include(od => od.Book).ThenInclude(b => b.Author).AsNoTracking().ToListAsync();


            return View(orderDetails);
        }
    }
}
