using ProductService.Domain;

namespace ProductService.Repository;

public interface IProductRepository
{
    Task<Product> GetProductById(string id);
    Task<List<Product>> GetProducts();
    Task<Product> CreateProduct(Product product);
    Task DeleteProduct(string id);
    Task<Product> UpdateProduct(Product product);
}