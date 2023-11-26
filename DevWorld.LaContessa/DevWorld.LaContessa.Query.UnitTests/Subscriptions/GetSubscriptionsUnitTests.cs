using FluentAssertions;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetSubscriptionsUnitTests : UnitTestBase
    {
        private GetSubscriptionsHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Initialize the in-memory database context and the handler
            _dbContext = new LaContessaDbContext(
                new LaContessaDbContextOptions
                {
                    DatabaseName = "lacontessadb",
                    UseInMemoryProvider = true,
                });

            _handler = new GetSubscriptionsHandler(_dbContext);
        }

        [Test]
        public async Task GetSubscriptionsHandler_ReturnsCorrectSubscriptions()
        {
            // Arrange
            var expectedSubscription1 = SubscriptionTestFactory.Create();
            var expectedSubscription2 = SubscriptionTestFactory.Create();

            _dbContext.Subscriptions.Add(expectedSubscription1);
            _dbContext.Subscriptions.Add(expectedSubscription2);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await _handler.Handle(new GetSubscriptions(), new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Subscriptions);

            response.Subscriptions
                .Should()
                .BeEquivalentTo(
                    new[] { expectedSubscription1, expectedSubscription2 },
                    options => options.Including(x => x.Id)
                                      .Including(x => x.UserId)
                                      .Including(x => x.Number)
                                      .Including(x => x.Valid)
                                      .ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
