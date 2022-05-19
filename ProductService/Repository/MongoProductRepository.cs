using System.Net;
using MongoDB.Driver;
using ProductService.Domain;

namespace ProductService.Repository;

public class MongoProductRepository : IProductRepository
{
    private IMongoDatabase _database;
    private IMongoCollection<Product> _collection;

    public MongoProductRepository(IMongoDatabase database)
    {
        _database = database;
        _collection = database.GetCollection<Product>("products");
    }

    public Task<Product> GetProductById(string id)
    {
        var product = _collection.AsQueryable().FirstOrDefault(x => x.ProductId == id);
      
        return Task.FromResult(product);
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = await _collection.Find(_ => true).ToListAsync() ;
        
        return products ?? new List<Product> { };
    }

    public async Task<Product> CreateProduct(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        
        await _collection.InsertOneAsync(product);

        return product;
    }

    public async Task DeleteProduct(string id)
    {
        await _collection.DeleteOneAsync(x => x.ProductId == id);
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var updateDefinition = Builders<Product>.Update
            .Set(p => p.ProductName, product.ProductName)
            .Set(p => p.ProductDescription, product.ProductDescription)
            .Set(p => p.ProductPrice, product.ProductPrice);
            
        await _collection.UpdateOneAsync(x => x.ProductId == product.ProductId, updateDefinition);

        return product;
    }
}