using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrbAuth.Web.ViewModels;

namespace CrbAuth.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult UserManagement()
        {
            var users = _userManager.Users;
            return View(users);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserVm)
        {
            if (ModelState.IsValid) return View(addUserVm);
            var user = new User()
            {
                UserName = addUserVm.UserName,
                FirstName = addUserVm.FirstName,
                MiddleInitial = addUserVm.MiddleInitial,
                LastName = addUserVm.LastName,
                Email = addUserVm.Email,
                EmailConfirmed = addUserVm.ConfirmedEmail
            };
            IdentityResult result = await _userManager.CreateAsync(user, addUserVm.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }

            return View(addUserVm);
        }

        public IActionResult EditUser()
        {
            throw new NotImplementedException();
        }

        public IActionResult DeleteUser()
        {
            throw new NotImplementedException();
        }




    }
}
