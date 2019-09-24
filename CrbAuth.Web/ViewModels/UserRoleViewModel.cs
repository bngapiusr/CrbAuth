using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrbAuth.Entities;
using Microsoft.AspNetCore.Identity;

namespace CrbAuth.Web.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<User>();
        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public List<User> Users { get; set; }
    }
}
