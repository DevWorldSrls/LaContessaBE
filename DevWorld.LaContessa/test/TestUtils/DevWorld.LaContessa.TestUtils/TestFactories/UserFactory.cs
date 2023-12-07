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
            Surname = Guid.NewGuid().ToString(),
            CardNumber = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            ImageProfile = Guid.NewGuid().ToString(),
            IsPro = new Random().Next(2) == 0,
            Password = new Random().Next().ToString()
        };

        configure?.Invoke(user);

        return user;
    }
}