using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Services;

namespace ForestGuardian.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddServiceDependency();
        return services;
    }

    public static IServiceCollection AddServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IOcorrenciaService, OcorrenciaService>();
        return services;
    }
}