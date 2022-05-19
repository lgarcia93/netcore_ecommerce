using ProductService.Domain;
using ProductService.Repository;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Product> GetProductById(string id)
    {
        return _repository.GetProductById(id);
    }

    public Task<List<Product>> GetProducts()
    {
        return _repository.GetProducts();
    }

    public Task<Product> CreateProduct(Product product)
    {
        return _repository.CreateProduct(product);
    }

    public Task DeleteProduct(string id)
    {
        return _repository.DeleteProduct(id);
    }

    public Task<Product> UpdateProduct(Product product)
    {
        return _repository.UpdateProduct(product);
    }
}