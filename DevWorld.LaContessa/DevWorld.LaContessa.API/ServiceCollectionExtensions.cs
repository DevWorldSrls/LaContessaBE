using DevWorld.LaContessa.API.Configuration;
using DevWorld.LaContessa.API.Middleware;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;

namespace DevWorld.LaContessa.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevWorld LaContessa API", Version = "v1" });
            c.CustomSchemaIds(x => x.FullName!.Replace("+", "."));
        });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddTransient<LaContessaProblemDetailsFactory>();

        //TODO evaluate to add healthChecks

        services.AddHttpContextAccessor();

        return services;
    }
}
