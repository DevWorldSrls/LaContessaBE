using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Users;

[TestFixture]
public class UpdateUserUnitTests : UnitTestBase
{
    [SetUp]
    public void Setup()
    {
        // Initialize the in-memory database context and the handler
        _dbContext = new LaContessaDbContext(
            new LaContessaDbContextOptions
            {
                DatabaseName = Guid.NewGuid().ToString(),
                UseInMemoryProvider = true
            });

        _handler = new UpdateUserHandler(_dbContext);
    }

    private UpdateUserHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task Handle_WhenUserExists_ShouldUpdateUser()
    {
        var startingUser = UserTestFactory.Create();

        _dbContext.Users.Add(startingUser);

        _dbContext.SaveChangesAsync();

        var updatedUser = UserTestFactory.Create();

        var UpdateUserRequest = new UpdateUser
        {
            User = new UpdateUser.UserDetail
            {
                Id = startingUser.Id,
                Email = updatedUser.Email,
                Name = updatedUser.Name
            }
        };

        await _handler.Handle(UpdateUserRequest, new CancellationToken());

        // Assert
        _dbContext.Users.ToList().Should().BeEquivalentTo(
            new[] { updatedUser },
            options => options
                .Including(x => x.Name)
                .Including(x => x.Email)
                .ExcludingMissingMembers()
        );
    }

    [Test]
    public async Task Handle_WhenUserNotFound_ShouldThrowUserNotFoundException()
    {
        var startingUser = UserTestFactory.Create();

        _dbContext.Users.Add(startingUser);

        _dbContext.SaveChangesAsync();

        var updatedUser = UserTestFactory.Create();

        var UpdateUserRequest = new UpdateUser
        {
            User = new UpdateUser.UserDetail
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                Name = updatedUser.Name
            }
        };

        // Assert
        Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(UpdateUserRequest, new CancellationToken()));
    }
}