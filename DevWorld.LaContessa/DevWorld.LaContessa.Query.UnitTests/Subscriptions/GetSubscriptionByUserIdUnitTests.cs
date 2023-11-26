using NUnit.Framework;
using FluentAssertions;
using DevWorld.LaContessa.Query;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetSubscriptionByUserIdUnitTests : UnitTestBase
    {
        private GetSubscriptionByUserIdHandler _handler;
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

            _handler = new GetSubscriptionByUserIdHandler(_dbContext);
        }

        [Test]
        public async Task GetSubscriptionHandler_ReturnsCorrectSubscription()
        {
            // Arrange
            var expectedSubscription = SubscriptionTestFactory.Create();

            _dbContext.Subscriptions.Add(expectedSubscription);
            await _dbContext.SaveChangesAsync();

            var request = new GetSubscriptionByUserId(expectedSubscription.UserId) { UserId = expectedSubscription.UserId };

            // Act
            var response = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Subscription);

            response.Subscription
                .Should()
                .BeEquivalentTo(
                    new GetSubscriptionByUserId.Response.SubscriptionDetail
                    {
                        Id = expectedSubscription.Id,
                        UserId = expectedSubscription.UserId,
                        Number = expectedSubscription.Number,
                        Valid = expectedSubscription.Valid
                    },
                    options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
