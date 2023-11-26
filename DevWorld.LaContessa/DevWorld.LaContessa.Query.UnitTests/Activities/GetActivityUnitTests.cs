using FluentAssertions;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetActivityHandlerTests : UnitTestBase
    {
        private GetActivityHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new LaContessaDbContext(
                new LaContessaDbContextOptions
                {
                    DatabaseName = "lacontessadb",
                    UseInMemoryProvider = true,
                });

            _handler = new GetActivityHandler(_dbContext);
        }

        [Test]
        public async Task GetActivityHandler_ReturnsCorrectBooking()
        {
            // Arrange
            var expectedActivity = ActivityTestFactory.Create(); // Ensure this sets the Id property

            _dbContext.Activities.Add(expectedActivity);
            await _dbContext.SaveChangesAsync();

            var request = new GetActivity(expectedActivity.Id) { Id = expectedActivity.Id };

            // Act
            var response = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Activity);

            response.Activity
                .Should()
                .BeEquivalentTo(
                    new GetActivity.Response.ActivityDetail()
                    {
                        Id = expectedActivity.Id,
                        Name = expectedActivity.Name,
                        Type = expectedActivity.Type,
                        IsAvaible = expectedActivity.IsAvaible
                    },
                        config => config.ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
