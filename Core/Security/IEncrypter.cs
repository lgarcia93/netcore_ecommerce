namespace Core.Security;

/// <summary>
/// Interface for defining PBKDF2 encryption process
/// </summary>
public interface IEncrypter
{
    string GetHash(string value);
    byte[] GetSalt();
}