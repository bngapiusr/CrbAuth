﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrbAuth.Entities
{

    public class Role
    {
        public Role(string name)
        {
            Name = name;
            UserRoles = new List<UserRole>();
        }
        [Key, Required]
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}