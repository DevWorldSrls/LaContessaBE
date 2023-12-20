using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests;

[TestFixture]
public class GetUserUnitTests : UnitTestBase
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

        _handler = new GetUserHandler(_dbContext);
    }

    private GetUserHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task GetUserHandler_ReturnsCorrectSubscription()
    {
        // Arrange
        var expectedUser = UserTestFactory.Create();

        _dbContext.Users.Add(expectedUser);
        await _dbContext.SaveChangesAsync();

        var request = new GetUser(expectedUser.Id) { Id = expectedUser.Id };

        // Act
        var response = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.User);

        response.User
            .Should()
            .BeEquivalentTo(
                new GetUser.Response.UserDetail
                {
                    Id = expectedUser.Id,
                    Name = expectedUser.Name,
                    Email = expectedUser.Email,
                    Surname = expectedUser.Surname,
                    CardNumber = expectedUser.CardNumber,
                    ImageProfile = expectedUser.ImageProfile,
                    IsPro = expectedUser.IsPro
                },
                options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
            );
    }
}