namespace Core.Auth;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpiryInMinutes { get; set; }
    public string Issuer { get; set; }

    public string Audience { get; set; }
}