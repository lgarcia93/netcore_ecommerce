using Core.Database.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Core.Database.MongoDb;

public static class Extension
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("mongo");

        var mongoConfig = new MongoConfig();
        
        configSection.Bind(mongoConfig);

        services.AddSingleton<IMongoClient>(serviceProvider => new MongoClient(mongoConfig.ConnectionString));
        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var client = serviceProvider.GetService<IMongoClient>();

            return client?.GetDatabase(mongoConfig.Database);
        });

        services.AddSingleton<IDatabaseInitializer, MongoInitializer>();
        
    }
}