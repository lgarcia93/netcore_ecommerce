using Core.SecretsManager.Model;

namespace Core.SecretsManager;

public interface IDatabaseCredentialsProvider
{
    public Task<DatabaseCredentials?> FetchCredentials(string secretName);
}