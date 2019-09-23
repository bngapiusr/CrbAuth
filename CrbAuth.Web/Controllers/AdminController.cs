using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IActionResult DeleteUser()
        {
            throw new NotImplementedException();
        }

        public IActionResult EditUser()
        {
            throw new NotImplementedException();
        }

        public IActionResult AddUser()
        {
            throw new NotImplementedException();
        }
    }
}
