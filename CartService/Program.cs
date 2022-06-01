using CartService.DbContext;
using CartService.Repository;
using CartService.Service;
using Core.Database.DynamoDB;using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySQL("server=localhost;database=cart;user=dbadmin;password=dev12345"));

builder.Services.TryAddScoped<ICartService, CartService.Service.CartService>();
builder.Services.TryAddScoped<DataContext, DataContext>();
builder.Services.TryAddScoped<IServiceDiscovery, ServiceDiscovery>();
builder.Services.TryAddScoped<IProductRepository, ProductRepository>();

builder.Services.TryAddScoped<ICartRepository, MySqlCartRepository>();
builder.Services.TryAddScoped<IProductService, ProductService>();

builder.Services.AddDynamoDb();
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", () => "Health Check");
app.MapControllers();

app.Run();