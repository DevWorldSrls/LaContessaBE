using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Persistance.Migrations;
using System.Reflection;

namespace DevWorld.LaContessa.SetupJob;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSetupJobServices(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddPersistance(
            configuration.GetSection("Persistance"),
            Assembly.GetAssembly(typeof(MigrationsAssemblyReferenceClass))!
            );

        services.AddJob();

        return services;
    }

    public static IServiceCollection AddJob(
        this IServiceCollection services
    )
    {
        services.AddScoped<IJob, MigrationJob>();

        services.AddHostedService<JobBackgroundService>();

        return services;
    }
}
