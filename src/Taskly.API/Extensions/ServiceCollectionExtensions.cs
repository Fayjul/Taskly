using System.Text;
using Taskly.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Taskly.Infrastructure.Repositories;
using Taskly.Infrastructure.Services;
using Taskly.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Taskly.API.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTasklyServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        // DbContext
        services.AddDbContext<TasklyDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); 

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        // Redis
        var redisConn = ConnectionMultiplexer.Connect(
            config.GetConnectionString("Redis")
        );
        services.AddSingleton<IConnectionMultiplexer>(redisConn);
        services.AddSingleton<ICacheService, RedisCacheService>();

        // JWT Auth
        var jwtSection = config.GetSection("Jwt");
        var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Secret"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSection["Issuer"],
                ValidAudience = jwtSection["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}



