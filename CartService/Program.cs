using CartService.Repository;
using CartService.Service;
using Core.Database.DynamoDB;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();

builder.Services.TryAddScoped<ICartService, CartService.Service.CartService>();

builder.Services.TryAddScoped<ICartRepository, DynamoDbCartRepository>();

builder.Services.AddDynamoDb();

app.MapGet("/", () => "Health Check");
app.MapControllers();
app.Run();