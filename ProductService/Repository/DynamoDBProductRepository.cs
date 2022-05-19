using Amazon.DynamoDBv2.DataModel;
using ProductService.Domain;

namespace ProductService.Repository;

public class DynamoDBProductRepository : IProductRepository
{
    private readonly IDynamoDBContext _dynamoDb;

    public DynamoDBProductRepository(IDynamoDBContext dynamoDb)
    {
        _dynamoDb = dynamoDb;
    }
    public Task<Product> GetProductById(string id)
    {
        return _dynamoDb.LoadAsync<Product>(id);
    }

    public  Task<List<Product>> GetProducts()
    {
        var productsSearch= _dynamoDb.ScanAsync<Product>(new ScanCondition[]{});

        return productsSearch.GetRemainingAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        product.ProductId = Guid.NewGuid().ToString();
        product.CreatedAt = DateTime.UtcNow;

        await _dynamoDb.SaveAsync(product);

        return product;
    }

    public Task DeleteProduct(string id)
    {
        return _dynamoDb.DeleteAsync<Product>(id);
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        await _dynamoDb.SaveAsync(product);

        return product;
    }
}