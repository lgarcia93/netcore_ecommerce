using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Core.Database.Interfaces;
using Core.Database.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Database.DynamoDB;

public static class Extension
{
    public static void AddDynamoDb(this IServiceCollection services)
    {
        var client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);

        services.AddSingleton<IAmazonDynamoDB>(client);
        
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
    }
}