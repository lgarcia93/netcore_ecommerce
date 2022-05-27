using Amazon.DynamoDBv2.DataModel;

namespace UserService.Model;

public class CreateUserModel
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
   
    public string Address { get; set; }
   
    public string BirthDay { get; set; }
    
    public string Email { get; set; }
    public string Role { get; set; }
}