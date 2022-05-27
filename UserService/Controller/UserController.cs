
using Core.Auth;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;
using UserService.Service;

namespace UserService.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthHandler _authHandler;

    public UserController(IUserService userService, IAuthHandler authHandler)
    {
        _userService = userService;
        _authHandler = authHandler;
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup([FromBody] CreateUserModel createUserModel)
    {
        var userExists = await _userService.UserNameExists(createUserModel.UserName);

        if (!userExists)
        {
            var createdUser = await _userService.CreateUser(createUserModel);

            var token = _authHandler.Create(createdUser);
            
            return Ok(new {User = createdUser, Auth = token});
        }

        return Conflict();
    }
}