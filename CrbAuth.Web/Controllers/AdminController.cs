using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                ModelState.AddModelError("", error.Description);
            }

            return View(addUserVm);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
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

            if (user != null)
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

            if (user != null)
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

        //Role Management
        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var role = new Role(vm.Name)
            {
                Name = vm.Name
            };

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            //check for null values - still working on this...
            //could use partialview 
            if (roleId == null)
            {
                ModelState.AddModelError("", "RoleId can not be null.");
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return RedirectToAction("RoleManagement", _roleManager.Roles);

            var vm = new EditRoleViewModel
            {
                RoleId = role.RoleId.ToString(),
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    vm.Users.Add(user.UserName);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel vm)
        {
            var role = await _roleManager.FindByIdAsync(vm.RoleId);
            if (role != null)
            {
                role.Name = vm.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement", _roleManager.Roles);
                }

                ModelState.AddModelError("", "Role not updated, something went wrong.");
                return View(vm);
            }

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            Role role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement", _roleManager.Roles);
                }

                ModelState.AddModelError("", "Something went wrong while deleting this role.");
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found.");
            }

            return View("RoleManagement", _roleManager.Roles);
        }

        //User in roles

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            var AddUserTooRoleVm = new UserRoleViewModel() {RoleId = role.RoleId.ToString()};
            foreach (var user in _userManager.Users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    AddUserTooRoleVm.Users.Add(user);
                }
            }

            return View(AddUserTooRoleVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            var role = await _roleManager.FindByIdAsync(vm.RoleId);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            var addUserToRoleVm = new UserRoleViewModel {RoleId = role.RoleId.ToString()};

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleVm.Users.Add(user);
                }
            }

            return View(addUserToRoleVm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            var role = await _roleManager.FindByIdAsync(vm.RoleId);

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(vm);

        }


    }
}