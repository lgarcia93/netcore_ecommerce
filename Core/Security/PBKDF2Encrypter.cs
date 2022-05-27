using System.Security.Cryptography;

namespace Core.Security;

public class Pbkdf2Encrypter : IEncrypter
{
   private readonly int _iterations = 1000;
   private readonly int _randomKeyBytes = 50;
    public string GetHash(string value)
    {
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(
            value, 
            GetSalt(),
            _iterations
        );
        
        return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(_randomKeyBytes));
    }

    public byte[] GetSalt()
    {
        var buffer = new Byte[_randomKeyBytes];
        
        var numberGenerator = RandomNumberGenerator.Create();
        
        numberGenerator.GetBytes(buffer);

        return buffer;
    }
}