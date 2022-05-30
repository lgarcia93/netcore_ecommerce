using Core.Auth;
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
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Health Check");

app.Run();