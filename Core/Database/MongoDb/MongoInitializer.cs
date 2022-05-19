using Core.Database.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Core.Database.MongoDb;

public class MongoInitializer : IDatabaseInitializer
{
    private bool _initialized;
    
    private IMongoDatabase _database;

    public MongoInitializer(IMongoDatabase database)
    {
        _database = database;
    }
    
    public async Task InitializeAsynchronously()
    {
        if (_initialized)
            return;

        IConventionPack conventionPack = new ConventionPack { 
            new IgnoreExtraElementsConvention(true),
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String)
        };
        
        ConventionRegistry.Register(
            "ecommerce",
            conventionPack,
            c => true
        );

        _initialized = true;
            
        await Task.CompletedTask;
    }
}