using Amazon.ServiceDiscovery;
using Amazon.ServiceDiscovery.Model;
using Core.ServiceDiscovery.Exceptions;
using Core.ServiceDiscovery.Interfaces;

namespace Core.ServiceDiscovery;

public class ServiceDiscovery : IServiceDiscovery
{
    public async Task<ServiceInfo> Discover(string serviceName)
    {
        var client = new AmazonServiceDiscoveryClient();

        var request = new DiscoverInstancesRequest
        {
            ServiceName = serviceName,
            NamespaceName = "ecommerce-app"
        };

        var instancesResponse = await client.DiscoverInstancesAsync(request);

        if (instancesResponse.Instances.Count == 0)
        {
            throw new NoServiceInstanceFound();
        }
        
        return new ServiceInfo
        {
            IP = instancesResponse.Instances.First().Attributes["AWS_INSTANCE_IPV4"],
            Port = instancesResponse.Instances.First().Attributes["SERVICE_PORT"]
        };
    }
}