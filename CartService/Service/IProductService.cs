using CartService.Model;

namespace CartService.Service;

public interface IProductService
{
    Task<Product> GetProductById(string productId);
}