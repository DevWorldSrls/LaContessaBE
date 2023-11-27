using DevWorld.LaContessa.Domain.Entities.Bookings;
using DevWorld.LaContessa.Domain.Entities.Users;

namespace DevWorld.LaContessa.TestUtils.TestFactories;

public class UserTestFactory
{
    public static User Create(Action<User>? configure = null, Guid? idOverride = null)
    {
        var id = idOverride ?? Guid.NewGuid();

        var user = new User
        {
            Id = id,
            Name = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            IsDeleted = false,
            // ...
        };

        configure?.Invoke(user);

        return user;
    }
}