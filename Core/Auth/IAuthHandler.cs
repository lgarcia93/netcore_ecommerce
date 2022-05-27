using Core.Entity;

namespace Core.Auth;

public interface IAuthHandler
{
    JwtAuthToken Create(User user);
}