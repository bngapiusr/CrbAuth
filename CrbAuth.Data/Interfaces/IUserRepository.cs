using CrbAuth.Entities;

namespace CrbAuth.Data.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {
        User FindByNormalizedUserName(string normalizedUserName);
        User FindByNormalizedEmail(string normalizedEmail);
    }
}