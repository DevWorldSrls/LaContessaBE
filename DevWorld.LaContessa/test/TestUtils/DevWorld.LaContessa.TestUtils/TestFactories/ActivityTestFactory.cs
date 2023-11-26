using DevWorld.LaContessa.Domain.Entities.Activities;

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
            Type = Guid.NewGuid().ToString(),
            Description = rnd.Next().ToString(),
            Services = new List<string> { new Random().Next().ToString(), new Random().Next().ToString() },
            Dates = new List<string> { new Random().Next().ToString(), new Random().Next().ToString() },
            IsAvaible = rnd.Next(2) == 0,
            IsDeleted = false,
            // ...
        };

        configure?.Invoke(activity);

        return activity;
    }
}