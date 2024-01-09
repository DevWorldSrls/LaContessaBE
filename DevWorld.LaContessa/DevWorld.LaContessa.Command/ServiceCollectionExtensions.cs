using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Activities;
using DevWorld.LaContessa.Command.Bookings;
using DevWorld.LaContessa.Command.Stripe;
using DevWorld.LaContessa.Command.Subscriptions;
using DevWorld.LaContessa.Command.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Command;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
        services.AddMediatR(x => { x.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)); });

        services.AddTransient<IRequestHandler<CreateUser>, CreateUserHandler>();
        services.AddTransient<IRequestHandler<UpdateUser>, UpdateUserHandler>();
        services.AddTransient<IRequestHandler<UpdateUserPassword>, UpdateUserPasswordHandler>();
        services.AddTransient<IRequestHandler<CreateSubscription>, CreateSubscriptionHandler>();
        services.AddTransient<IRequestHandler<UpdateSbscription>, UpdateSubscriptionHandler>();
        services.AddTransient<IRequestHandler<CreateBooking>, CreateBookingHandler>();
        services.AddTransient<IRequestHandler<UpdateBooking>, UpdateBookingHandler>();
        services.AddTransient<IRequestHandler<CreateActivity>, CreateActivityHandler>();
        services.AddTransient<IRequestHandler<UpdateActivity>, UpdateActivityHandler>();

        services.AddTransient<IRequestHandler<CreateCard>, CreateCardHandler>();
        services.AddTransient<IRequestHandler<DeleteCard>, DeleteCardHandler>();
        services.AddTransient<IRequestHandler<CreateStripePaymentRequest>, CreateStripePaymentRequestHandler>();

        return services;
    }
}
