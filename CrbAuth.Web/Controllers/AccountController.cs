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
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //why return url
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByNameAsync(vm.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return RedirectToAction("Index","Home");
                    }

                    return Redirect(vm.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "UserName/Password not found");
            return View(vm);
        }
    }
}
