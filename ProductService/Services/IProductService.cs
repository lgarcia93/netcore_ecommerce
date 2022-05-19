using ProductService.Domain;

namespace ProductService.Services;

public interface IProductService
{
    Task<Product> GetProductById(string id);
    Task<List<Product>> GetProducts();
    Task<Product> CreateProduct(Product product);
    Task DeleteProduct(string id);
    Task<Product> UpdateProduct(Product product);
}