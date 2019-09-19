using System.Collections.Generic;
using CrbAuth.Entities;

namespace CrbAuth.Data.Interfaces
{
    public interface IUserRoleRepository
    {
        void Add(string userId, string roleName);
        void Remove(string userId, string roleName);
        IEnumerable<string> GetRoleNamesByUserId(string userId);
        IEnumerable<User> GetUsersByRoleName(string roleName);
    }
}