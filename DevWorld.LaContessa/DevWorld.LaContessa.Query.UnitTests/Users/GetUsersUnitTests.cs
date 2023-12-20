using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.Query.Users;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests.Users;

[TestFixture]
public class GetUsersUnitTests : UnitTestBase
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

        _handler = new GetUsersHandler(_dbContext);
    }

    private GetUsersHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task GetUsersHandler_ReturnsCorrectSubscriptions()
    {
        // Arrange
        var expectedUser1 = UserTestFactory.Create();
        var expectedUser2 = UserTestFactory.Create();

        _dbContext.Users.Add(expectedUser1);
        _dbContext.Users.Add(expectedUser2);
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _handler.Handle(new GetUsers(), new CancellationToken());

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Users);

        response.Users
            .Should()
            .BeEquivalentTo(
                new[] { expectedUser1, expectedUser2 },
                options => options.Including(x => x.Id)
                    .Including(x => x.Name)
                    .Including(x => x.Email)
                    .Including(x => x.Surname)
                    .Including(x => x.CardNumber)
                    .Including(x => x.ImageProfile)
                    .Including(x => x.IsPro)
                    .ExcludingMissingMembers() // Exclude fields that are not part of the response
            );
    }
}