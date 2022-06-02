using Amazon.SQS;
using Amazon.SQS.Model;

namespace Core.SQS;

public class SQSManager
{
    public async Task SendMessage(string queueUrl, string messageBody)
    {
        var client = new AmazonSQSClient();
        var request = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = messageBody
        };

        await client.SendMessageAsync(request);
    }
}