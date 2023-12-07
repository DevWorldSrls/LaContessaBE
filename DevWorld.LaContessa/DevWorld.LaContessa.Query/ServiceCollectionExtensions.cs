using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Query;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueryServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<IRequestHandler<GetUsers, GetUsers.Response>, GetUsersHandler>();
        services.AddTransient<IRequestHandler<GetSubscriptions, GetSubscriptions.Response>, GetSubscriptionsHandler>();
        services.AddTransient<IRequestHandler<GetSubscription, GetSubscription.Response>, GetSubscriptionHandler>();
        services
            .AddTransient<IRequestHandler<GetSubscriptionByUserId, GetSubscriptionByUserId.Response>,
                GetSubscriptionByUserIdHandler>();
        services.AddTransient<IRequestHandler<GetBookings, GetBookings.Response>, GetBookingsHandler>();
        services.AddTransient<IRequestHandler<GetBooking, GetBooking.Response>, GetBookingHandler>();
        services
            .AddTransient<IRequestHandler<GetBookingByUserId, GetBookingByUserId.Response>,
                GetBookingByUserIdHandler>();
        services.AddTransient<IRequestHandler<GetActivities, GetActivities.Response>, GetActivitiesHandler>();
        services.AddTransient<IRequestHandler<GetActivity, GetActivity.Response>, GetActivityHandler>();

        return services;
    }
}
