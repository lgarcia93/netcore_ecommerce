using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using OrderService.Entity;
using OrderService.Model;
using OrderService.Service;

namespace OrderService.Worker;

public class SQSOrdersBackgroundWorker : BackgroundService
{
    private readonly IOrderService _orderService;

    public SQSOrdersBackgroundWorker(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        

        await ProcessMessages(stoppingToken);
    }

    private async Task ProcessMessages(CancellationToken stoppingToken)
    {
        var client = new AmazonSQSClient();
        
        var awsAccountId = Environment.GetEnvironmentVariable("AWS_ACCOUNT_ID");

        var queueUrl = $"https://sqs.us-east-1.amazonaws.com/{awsAccountId}/checkout-queue";
        
        var request = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var messagesResponse = await client.ReceiveMessageAsync(request);

            if (messagesResponse.Messages.Any())
            {
                foreach (var message in messagesResponse.Messages)
                {
                    var orderModel =JsonSerializer.Deserialize<List<OrderCreationModel>>(message.Body);

                    if (orderModel != null && !orderModel.Any())
                    {
                        continue;
                        
                    }
                    
                    await _orderService.CreateOrder(new Order
                    {
                        OrderDate = DateTime.Now,
                        UserId = orderModel[0].UserId,
                        OrderProducts = orderModel.ConvertAll(cartItem => new OrderProduct
                        {
                            ProductId = cartItem.ProductId,
                            ProductName = cartItem.ProductName,
                            ProductDescription = cartItem.ProductDescription,
                        }).ToList(),
                    });
                }
            }
        }
    }
}