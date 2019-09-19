using Microsoft.AspNetCore.Identity;

namespace CrbAuth.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        //IRepository<UserToken,UserTokenKey> UserTokenRepository { get; }
        IUserRepository UrserRoleRepository { get; }

        void Commit();

    }
}