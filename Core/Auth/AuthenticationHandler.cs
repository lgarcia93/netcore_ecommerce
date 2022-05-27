using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Auth;

public class AuthenticationHandler : IAuthHandler
{
    private readonly IConfiguration _configuration;
    
    public AuthenticationHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public JwtAuthToken Create(User user)
    {
        var jwtOptions = new JwtOptions();
        _configuration.GetSection("jwt").Bind(jwtOptions);
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.StreetAddress, user.Address),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDay),
        };

        var expiryDate = DateTime.Now.AddMinutes(jwtOptions.ExpiryInMinutes);
        
        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims,
            expires: expiryDate,
            signingCredentials: credentials
        );

        return new JwtAuthToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expiryDate,
        };
    }
}
