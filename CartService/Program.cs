using CartService.DbContext;
using CartService.Repository;
using CartService.Service;
using Core.Database.DynamoDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySQL("server=localhost;database=cart;user=user;password=password"));

builder.Services.TryAddScoped<ICartService, CartService.Service.CartService>();
builder.Services.TryAddScoped<DataContext, DataContext>();

builder.Services.TryAddScoped<ICartRepository, MySqlCartRepository>();

builder.Services.AddDynamoDb();

var app = builder.Build();

app.MapGet("/", () => "Health Check");
app.MapControllers();
app.Run();