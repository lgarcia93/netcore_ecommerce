namespace Core.SecretsManager.Model;

public class DatabaseCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string IP { get; set; }
    public string Port { get; set; }
    public string DBName { get; set; }
    public string DbInstanceIdentifier { get; set; }
}