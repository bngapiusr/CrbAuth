using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;
using CrbAuth.Web.ViewModels;

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = vm.UserName,
                    Email =vm.Email,
                    EmailConfirmed =vm.ConfirmedEmail,
                    PasswordHash = vm.Password
                 };
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
