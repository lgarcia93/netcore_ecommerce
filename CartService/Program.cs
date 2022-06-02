using System.Text;
using CartService.DbContext;
using CartService.Repository;
using CartService.Service;
using Core.Auth;
using Core.Database.DynamoDB;
using Core.SecretsManager;
using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;


var builder = WebApplication.CreateBuilder(args);

//Will move to Secrets Manager soon
var connectionString = "server=localhost;database=cart;user=root;password=dev12345";

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.TryAddScoped<ICartService, CartService.Service.CartService>();
builder.Services.TryAddScoped<DataContext, DataContext>();
builder.Services.TryAddScoped<IServiceDiscovery, ServiceDiscovery>();
builder.Services.TryAddScoped<IProductRepository, ProductRepository>();
builder.Services.TryAddScoped<IDatabaseCredentialsProvider,AwsSecretsManagerDatabaseCredentialsProvider>();

builder.Services.TryAddScoped<ICartRepository, MySqlCartRepository>();
builder.Services.TryAddScoped<IProductService, ProductService>();

builder.Services.AddAuthorization();

builder.Services.AddJwt(builder.Configuration);

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(o =>
//     {
//         o.RequireHttpsMetadata = false;
//         o.SaveToken = false;
//         o.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = false,
//             ValidateIssuer = false,
//             ValidateAudience = false,
//             ValidateLifetime = false,
//             ClockSkew = TimeSpan.Zero,
//             ValidIssuer = "http://localhost",
//             ValidAudience = "http://localhost",
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VPs4pewMoU5Cwtf7bxM58mxU677aKZqs"))
//         };
//     });

builder.Services.AddControllers();
builder.Services.AddDynamoDb();
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", () => "Health Check");

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();