using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Security;

public static class Extension
{
    public static void AddEncrypter(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IEncrypter, Pbkdf2Encrypter>();
    }
    
}