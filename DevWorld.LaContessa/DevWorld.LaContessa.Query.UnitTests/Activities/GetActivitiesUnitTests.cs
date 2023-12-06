using FluentAssertions;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetActivitiesUnitTests : UnitTestBase
    {
        private GetActivitiesHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Initialize the in-memory database context and the handler
            _dbContext = new LaContessaDbContext(
                new LaContessaDbContextOptions
                {
                    DatabaseName = Guid.NewGuid().ToString(),
                    UseInMemoryProvider = true,
                });

            _handler = new GetActivitiesHandler(_dbContext);
        }

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
}
