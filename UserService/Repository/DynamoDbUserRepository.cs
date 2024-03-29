using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Core.Entity;
using Core.Security;
using UserService.Model;

namespace UserService.Repository;

public class DynamoDbUserRepository : IUserRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;
    private readonly IEncrypter _encrypter;
    private readonly IAmazonDynamoDB _dynamoDb;

    public DynamoDbUserRepository(
        IDynamoDBContext dynamoDbContext,
        IEncrypter encrypter, IAmazonDynamoDB dynamoDb
        )
    {
        _dynamoDbContext = dynamoDbContext;
        _encrypter = encrypter;
        _dynamoDb = dynamoDb;
    }
    
    public async Task<User> CreateUser(CreateUserModel user)
    {
        //var hashedPassword = _encrypter.GetHash(user.Password);
       
        var newUser = new User
        {
            UserId = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            Address = user.Address,
            Email = user.Email,
            BirthDay = user.BirthDay,
            UserName = user.UserName,
            Password = user.Password,//hashedPassword,
            Role = user.Role
        };

        await _dynamoDbContext.SaveAsync(newUser);

        return newUser;
    }

    public  Task<User> GetUserById(string userId)
    {
        return _dynamoDbContext.LoadAsync<User>(userId);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        var keyExpression = "UserName = :UserName";
        var keyConditions = new Dictionary<string, AttributeValue>();
        
        keyConditions.Add(":UserName", new AttributeValue{S = userName});
        
        var request = new QueryRequest
        {
            TableName = "users",
            IndexName = "UserNameIndex",
            KeyConditionExpression = keyExpression,
            ExpressionAttributeValues = keyConditions
        };
        var response = await _dynamoDb.QueryAsync(request);

        var item = response.Items.FirstOrDefault();
        
        if (item != null)
        {
            var user = new User
            {
                Address = item["Address"].S,
                Email = item["Email"].S,
                BirthDay = item["BirthDay"].S,
                CreatedAt = DateTime.Parse(item["CreatedAt"].S),
                UserId = item["UserId"].S,
                UserName = item["UserName"].S,  
                Password = item["Password"].S,
                Role = item["Role"].S
            };

            return user;
        }

        return null;
    }

    public async Task<bool> UserNameExists(string userName)
    {
        var user = await GetUserByUserName(userName);

        if (user == null)
        {
            return false;
        }

        return true;
    }
}