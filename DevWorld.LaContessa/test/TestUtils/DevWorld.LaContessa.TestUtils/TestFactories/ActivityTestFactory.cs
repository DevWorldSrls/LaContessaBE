using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Query.Abstractions;

namespace DevWorld.LaContessa.TestUtils.TestFactories;

public class ActivityTestFactory
{
    public static Activity Create(Action<Activity>? configure = null, Guid? idOverride = null)
    {
        var id = idOverride ?? Guid.NewGuid();

        var rnd = new Random();

        var activity = new Activity
        {
            Id = id,
            Name = rnd.Next().ToString(),
            IsOutdoor = rnd.Next(2) == 0,
            Description = rnd.Next().ToString(),
            ActivityImg = rnd.Next().ToString(),
            ServiceList = new List<Service>(),
            DateList = new List<ActivityDate>()
        };

        configure?.Invoke(activity);

        return activity;
    }
}