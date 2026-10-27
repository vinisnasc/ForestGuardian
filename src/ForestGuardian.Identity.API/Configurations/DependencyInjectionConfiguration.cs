using ForestGuardian.Identity.API.Data;
using ForestGuardian.Identity.API.Interfaces;
using ForestGuardian.Identity.API.Services;
using Microsoft.EntityFrameworkCore;

namespace ForestGuardian.Identity.API.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddServiceDependency();
        return services;
    }

    public static IServiceCollection AddServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}