using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace DevWorld.LaContessa.Stripe;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStripeServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

        return services
            .AddScoped<CustomerService>()
            .AddScoped<PaymentIntentService>()
            .AddScoped<PaymentMethodService>()
            .AddScoped<RefundService>()
            .AddScoped<IStripeAppService, StripeAppService>();
    }
}

