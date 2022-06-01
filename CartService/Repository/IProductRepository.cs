using CartService.Model;

namespace CartService.Repository;

public interface IProductRepository
{
    Task<Product> GetProductById(string productId);
}