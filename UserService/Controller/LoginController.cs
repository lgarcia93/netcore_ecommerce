using Core.Auth;
using Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;
using UserService.Service;

namespace UserService.Controller;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthHandler _authHandler;

    public LoginController(IUserService userService, IAuthHandler authHandler)
    {
        _userService = userService;
        _authHandler = authHandler;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var user = await _userService.GetUserByUserName(loginModel.Username);

        if (user != null)
        {
            if (user.Password == loginModel.Password)
            {
                var token = _authHandler.Create(user);
                return Ok(token);
            }
        }

        return NotFound("User not found");
    }
}