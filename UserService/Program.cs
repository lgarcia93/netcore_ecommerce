using Core.Database.DynamoDB;
using Core.Security;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UserService.Repository;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.TryAddScoped<IUserService, UserService.Service.UserService>();

builder.Services.TryAddScoped<IUserRepository, DynamoDbUserRepository>();

builder.Services.TryAddSingleton<IEncrypter, Pbkdf2Encrypter>();

builder.Services.AddDynamoDb();

var app = builder.Build();

app.MapControllers();

app.Run();