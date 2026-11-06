using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Infra.Context;
using ForestGuardian.Infra.Repository;
using ForestGuardian.Services;
using Microsoft.EntityFrameworkCore;

namespace ForestGuardian.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ForestGuardianContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("ForestGuardian.Infra")));

        services.AddServiceDependency();
        services.AddRepositoryDependency();
        return services;
    }

    public static IServiceCollection AddServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IOcorrenciaService, OcorrenciaService>();
        services.AddScoped<INotificator, Notificator>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}