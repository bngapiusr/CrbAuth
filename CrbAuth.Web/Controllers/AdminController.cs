using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrbAuth.Web.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user==null)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }
            
            var vm = new AddUserViewModel()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.Email,
                MiddleInitial = user.MiddleInitial,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(AddUserViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId.ToString());

            if (user !=null)
            {
                user.UserId = vm.UserId;
                user.UserName = vm.UserName;
                user.Email = vm.Email;
                user.MiddleInitial = vm.MiddleInitial;
                user.LastName = vm.LastName;
                user.Email = vm.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement", _userManager.Users);
                }
                ModelState.AddModelError("", "User not updated, something went wrong.");
                return View(vm);
            }

            return RedirectToAction("UserManagement", _userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user!=null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
                }
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found.");
            }

            return View("UserManagement", _userManager.Users);
        }

        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult DeleteRole()
        {
            throw new NotImplementedException();
        }

        public IActionResult EditRole()
        {
            throw new NotImplementedException();
        }

        public IActionResult AddNewRole()
        {
            throw new NotImplementedException();
        }
    }
}
