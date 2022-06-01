using CartService.Model;
using CartService.Repository;

namespace CartService.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public Task<Product> GetProductById(string productId)
    {
        return _productRepository.GetProductById(productId);
    }
}