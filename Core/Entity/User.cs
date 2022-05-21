using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace Core.Entity;

[DynamoDBTable("users")]
public class User
{
    [DynamoDBHashKey]
    [DynamoDBProperty(AttributeName = "UserId")]
    public string UserId { get; set; }
    [DynamoDBProperty(AttributeName = "UserName")]
    public string UserName { get; set; }
    [DynamoDBProperty(AttributeName = "Address")]
    public string Address { get; set; }
    [DynamoDBProperty(AttributeName = "BirthDay")]
    public string BirthDay { get; set; }
    [DynamoDBProperty(AttributeName = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    [DynamoDBProperty(AttributeName = "Password")]
    public string Password { get; set; }
    
    [DynamoDBProperty(AttributeName = "Email")]
    public string Email { get; set; }

}