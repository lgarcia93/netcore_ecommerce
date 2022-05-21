using Core.Entity;
using UserService.Model;

namespace UserService.Repository;

public interface IUserRepository
{
    Task<User> CreateUser(CreateUserModel user);
    Task<User> GetUserById(string userId);

    Task<User?> GetUserByUserName(string userName);

    Task<bool> UserNameExists(string userName);
}