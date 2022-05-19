using Core.Database.DynamoDB;
using ProductService.Repository;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService.Services.ProductService>();
//builder.Services.AddScoped<IProductRepository, MongoProductRepository>();

builder.Services.AddScoped<IProductRepository, DynamoDBProductRepository>();

//builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddDynamoDb();

var app = builder.Build();
    
//var appBuilder = ((IApplicationBuilder)app);

// var dbInitializer = appBuilder.ApplicationServices.GetService<IDatabaseInitializer>();
//
// dbInitializer?.InitializeAsynchronously();

app.MapControllers();

app.Run();