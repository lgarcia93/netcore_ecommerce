namespace Core.ServiceDiscovery.Interfaces;

public interface IServiceDiscovery
{
    Task<ServiceInfo> Discover(string serviceName);
}