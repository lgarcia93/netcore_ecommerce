using CartService.Model;
using Core.ServiceDiscovery.Interfaces;
using RestSharp;

namespace CartService.Repository;

public class ProductRepository : IProductRepository
{
    private readonly  IServiceDiscovery _serviceDiscovery;
    private readonly string _productServiceName = "product-service";
    public ProductRepository(IServiceDiscovery serviceDiscover)
    {
        _serviceDiscovery = serviceDiscover;
    }
    
    public async Task<Product> GetProductById(string productId)
    {
        var serviceInfo = await _serviceDiscovery.Discover(_productServiceName);

        var client = new RestClient(
            String.Format(
                "http://{0}:{1}/api/product/{2}",
               // serviceInfo.IP,
                "localhost",
               8082,
                //serviceInfo.Port,
                productId)
            );

        var request = new RestRequest();

        return await client.GetAsync<Product>(request);
    }
}