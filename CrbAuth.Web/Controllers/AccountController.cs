using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;

namespace CrbAuth.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login(string returnUrl)
        {
            //why return url
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

    }
}
