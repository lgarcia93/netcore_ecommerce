
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Auth;

public static class Extension
{
    public static void AddJwt(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = new JwtOptions();
        
        configuration.GetSection("jwt").Bind(options);

        serviceCollection.AddSingleton<IAuthHandler, AuthenticationHandler>();
        serviceCollection.AddAuthentication().AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = options.Issuer,
                ValidateIssuer = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
            };
        });
    }
}