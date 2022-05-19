using Amazon.DynamoDBv2.DataModel;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Domain;

[DynamoDBTable("products")]
public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    [DynamoDBHashKey]
    [DynamoDBProperty(AttributeName = "ProductId")]
    public string? ProductId { get; set; }
    [DynamoDBProperty(AttributeName = "ProductName")]
    public string ProductName { get; set; }
    
    [DynamoDBProperty(AttributeName = "ProductDescription")]
    public string ProductDescription { get; set; }
    
    [DynamoDBProperty(AttributeName = "ProductPrice")]
    public float ProductPrice { get; set; }
    
    [DynamoDBProperty(AttributeName = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
}