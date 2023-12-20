using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activities;
using DevWorld.LaContessa.Query.Activity;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests.Activities;

[TestFixture]
public class GetActivitiesUnitTests : UnitTestBase
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

        _handler = new GetActivitiesHandler(_dbContext);
    }

    private GetActivitiesHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task GetActivitiesHandler_ReturnsCorrectSubscriptions()
    {
        // Arrange
        var expectedActivity1 = ActivityTestFactory.Create();
        var expectedActivity2 = ActivityTestFactory.Create();

        _dbContext.Activities.Add(expectedActivity1);
        _dbContext.Activities.Add(expectedActivity2);
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _handler.Handle(new GetActivities(), new CancellationToken());

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Activities);

        response.Activities
            .Should()
            .BeEquivalentTo(
                new[] { expectedActivity1, expectedActivity2 },
                options => options.Including(x => x.Id)
                    .Including(x => x.Name)
                    .Including(x => x.IsOutdoor)
                    .Including(x => x.Description)
                    .Including(x => x.ActivityImg)
                    .Including(x => x.ServiceList)
                    .Including(x => x.DateList)
                    .ExcludingMissingMembers() // Exclude fields that are not part of the response
            );
    }
}