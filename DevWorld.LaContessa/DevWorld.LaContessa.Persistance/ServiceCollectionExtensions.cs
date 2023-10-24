using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DevWorld.LaContessa.Persistance;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly migrationAssembly)
    {
        services.AddDbContext<LaContessaDbContext>();
        services.AddOptions<LaContessaDbContextOptions>()
            .Bind(configuration)
            .Configure(x => x.MigrationsAssembly = migrationAssembly);
        services.AddScoped(x => x.GetRequiredService<IOptionsSnapshot<LaContessaDbContextOptions>>().Value);

        return services;
    }
}
