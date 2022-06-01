using Amazon.ServiceDiscovery;
using Amazon.ServiceDiscovery.Model;

namespace Core.ServiceDiscovery;

public class ServiceDiscovery
{
    public async Task Discover()
    {
        var client = new AmazonServiceDiscoveryClient();

        var request = new DiscoverInstancesRequest();
        request.ServiceName = "ecommerce-service";
        request.NamespaceName = "ecommerce-app";
        
        var instancesResponse = await client.DiscoverInstancesAsync(request);

        foreach (var instanceSummary in instancesResponse.Instances)
        {
           Console.Write(instanceSummary.Attributes);
        }
        
     
    }
}