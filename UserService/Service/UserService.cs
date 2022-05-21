using Core.Entity;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository userRepository)
    {
        _repository = userRepository;
    }
    
    public Task<User> CreateUser(CreateUserModel user)
    {
        return _repository.CreateUser(user);
    }

    public Task<User> GetUserById(string userId)
    {
        return _repository.GetUserById(userId);
    }

    public Task<User?> GetUserByUserName(string userName)
    {
        return _repository.GetUserByUserName(userName);
    }

    public Task<bool> UserNameExists(string userName)
    {
        return _repository.UserNameExists(userName);
    }
}