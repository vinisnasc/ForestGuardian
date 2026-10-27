using ForestGuardian.Identity.API.Data;
using ForestGuardian.Identity.API.Extensions;
using ForestGuardian.Identity.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ForestGuardian.Identity.API.Configurations;

public static class AuthConfiguration
{
    public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("IdentityConfiguration");
        services.Configure<IdentityConfiguration>(appSettingsSection);

        var identityConfiguration = appSettingsSection.Get<IdentityConfiguration>();
        var key = Encoding.ASCII.GetBytes(identityConfiguration.Secret);

        services.AddIdentity<UsuarioAplicacao, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOpt =>
        {
            bearerOpt.RequireHttpsMetadata = true;
            bearerOpt.SaveToken = true;
            bearerOpt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = identityConfiguration.ValidoEm,
                ValidIssuer = identityConfiguration.Emissor
            };
        });

        return services;
    }


}