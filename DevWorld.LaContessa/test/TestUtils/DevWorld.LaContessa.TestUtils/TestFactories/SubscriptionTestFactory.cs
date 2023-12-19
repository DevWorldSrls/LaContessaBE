using DevWorld.LaContessa.Domain.Entities.Subscriptions;

namespace DevWorld.LaContessa.TestUtils.TestFactories;

public class SubscriptionTestFactory
{
    public static Subscription Create(Action<Subscription>? configure = null, Guid? idOverride = null)
    {
        var id = idOverride ?? Guid.NewGuid();

        var user = UserTestFactory.Create();
        var activity = ActivityTestFactory.Create();

        var subscription = new Subscription
        {
            Id = id,
            User = user,
            Activity = activity,
            CardNumber = new Random().Next(),
            Valid = new Random().Next(2) == 0,
            ExpirationDate = Guid.NewGuid().ToString(),
            SubscriptionType = Guid.NewGuid().ToString()
        };

        configure?.Invoke(subscription);

        return subscription;
    }
}
