using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Core.SecretsManager.Model;

namespace Core.SecretsManager;

public class AwsSecretsManagerDatabaseCredentialsProvider : IDatabaseCredentialsProvider
{
    public async Task<DatabaseCredentials?> FetchCredentials(string secretName)
    {
        var client = new AmazonSecretsManagerClient();
        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = await client.GetSecretValueAsync(request);

        var databaseCredentials = JsonSerializer.Deserialize<DatabaseCredentials>(response.SecretString);

        return databaseCredentials;
    }
}