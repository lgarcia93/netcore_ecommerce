using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;
using UserService.Repository;
using UserService.Service;

namespace UserService.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup([FromBody] CreateUserModel createUserModel)
    {
        var userExists = await _userService.UserNameExists(createUserModel.UserName);

        if (!userExists)
        {
            var createdUser = await _userService.CreateUser(createUserModel);
            
            return Ok(createdUser);
        }

        return Conflict();
    }

    [HttpPost]
    public Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        
        
        return Task.FromResult<IActionResult>(Ok());
    }
}