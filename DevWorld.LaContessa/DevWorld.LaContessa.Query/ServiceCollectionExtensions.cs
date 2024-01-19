using DevWorld.LaContessa.Query.Abstractions.Activities;
using DevWorld.LaContessa.Query.Abstractions.Banners;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using DevWorld.LaContessa.Query.Abstractions.Stripe;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.Query.Activity;
using DevWorld.LaContessa.Query.Banners;
using DevWorld.LaContessa.Query.Bookings;
using DevWorld.LaContessa.Query.Services;
using DevWorld.LaContessa.Query.Stripe;
using DevWorld.LaContessa.Query.Subscriptions;
using DevWorld.LaContessa.Query.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevWorld.LaContessa.Query;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueryServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<ITokenService, TokenService>();

        services.AddTransient<IRequestHandler<GetUsers, GetUsers.Response>, GetUsersHandler>();
        services.AddTransient<IRequestHandler<GetUser, GetUser.Response>, GetUserHandler>();
        services.AddTransient<IRequestHandler<LoginRequest, GetUser.Response>, LoginRequestHandler>();
        services.AddTransient<IRequestHandler<RefreshTokenRequest, GetUser.Response>, RefreshTokenRequestHandler>();

        services.AddTransient<IRequestHandler<GetSubscriptions, GetSubscriptions.Response>, GetSubscriptionsHandler>();
        services.AddTransient<IRequestHandler<GetSubscription, GetSubscription.Response>, GetSubscriptionHandler>();
        services.AddTransient<IRequestHandler<GetSubscriptionByUserId, GetSubscriptionByUserId.Response>, GetSubscriptionByUserIdHandler>();

        services.AddTransient<IRequestHandler<GetBookings, GetBookings.Response>, GetBookingsHandler>();
        services.AddTransient<IRequestHandler<GetBooking, GetBooking.Response>, GetBookingHandler>();
        services.AddTransient<IRequestHandler<GetBookingByUserId, GetBookingByUserId.Response>, GetBookingByUserIdHandler>();

        services.AddTransient<IRequestHandler<GetActivities, GetActivities.Response>, GetActivitiesHandler>();
        services.AddTransient<IRequestHandler<GetActivity, GetActivity.Response>, GetActivityHandler>();

        services.AddTransient<IRequestHandler<GetBanners, GetBanners.Response>, GetBannersHandler>();
        services.AddTransient<IRequestHandler<GetBanner, GetBanner.Response>, GetBannerHandler>();

        services.AddTransient<IRequestHandler<GetCard, GetCard.Response>, GetCardHandler>();

        return services;
    }
}
