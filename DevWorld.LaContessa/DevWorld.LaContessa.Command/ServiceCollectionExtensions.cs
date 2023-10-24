using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Command;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        });

        services.AddTransient<IRequestHandler<CreateUser>, CreateUserHandler>();

        return services;
    }
}
