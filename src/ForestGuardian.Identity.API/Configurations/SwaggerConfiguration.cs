namespace ForestGuardian.Identity.API.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
            {
                Title = "Identificacao Consulta",
                Description = "API de gerenciamento de login do projeto ForestGuardian"
            });
        });

        return services;
    }
}