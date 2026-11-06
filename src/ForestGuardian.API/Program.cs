using ForestGuardian.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration(builder.Environment);
app.Run();