using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoProject.Core.Config;

namespace ToDoProject.Api.Extension;

public static class JwtServiceExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = EnvironmentConfig.JwtIssuer,
                    ValidAudience = EnvironmentConfig.JwtAudience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentConfig.JwtKey ?? string.Empty))
                };
            });
    }

    public static void AddCustomAuthorization(this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy("UserPolicy", policy => policy.RequireRole("ROLE_USER"))
            .AddPolicy("AdminPolicy", policy => policy.RequireRole("ROLE_ADMIN"));
    }
}