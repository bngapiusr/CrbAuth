using CrbAuth.Entities;

namespace CrbAuth.Data.Interfaces
{
    public interface IRoleRepository : IRepository<Role, string>
    {
        Role FindBName(string roleName);

    }
}