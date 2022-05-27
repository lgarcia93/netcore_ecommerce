namespace Core.Auth;

public class JwtAuthToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}