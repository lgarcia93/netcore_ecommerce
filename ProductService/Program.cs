using Core.Database.DynamoDB;
using Core.Security;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductService.Repository;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEncrypter();
builder.Services.TryAddScoped<IProductService, ProductService.Services.ProductService>();

builder.Services.TryAddScoped<IProductRepository, DynamoDBProductRepository>();

builder.Services.AddDynamoDb();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Health Check");

app.Run();