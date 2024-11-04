using Microsoft.EntityFrameworkCore;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Application.Service.Concrete;
using ToDoProject.Core.Config;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.Core.Service.Concrete;
using ToDoProject.CrossCutting.Logger.Abstract;
using ToDoProject.CrossCutting.Logger.Concrete;
using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.DataAccess.Repository.Concrete;

namespace ToDoProject.Api.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        
        return services;
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        
        return services;
    }

    public static IServiceCollection AddCrossCuttingServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        
        return services;
    }

    public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        return services;
    }

    public static IServiceCollection AddDatabaseConnections(this IServiceCollection services)
    {
        EnvironmentConfig.LoadEnv();

        services.AddDbContext<PostgreSqlDbContext>(options =>
            options.UseNpgsql(EnvironmentConfig.PostgreSqlConnection));

        return services;
    }
}