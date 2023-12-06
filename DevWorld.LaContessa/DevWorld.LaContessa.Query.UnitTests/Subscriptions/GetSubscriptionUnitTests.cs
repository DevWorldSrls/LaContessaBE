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
    public class GetSubscriptionUnitTests : UnitTestBase
    {
        private GetSubscriptionHandler _handler;
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

            _handler = new GetSubscriptionHandler(_dbContext);
        }

        [Test]
        public async Task GetSubscriptionHandler_ReturnsCorrectSubscription()
        {
            // Arrange
            var expectedSubscription = SubscriptionTestFactory.Create();

            _dbContext.Subscriptions.Add(expectedSubscription);
            await _dbContext.SaveChangesAsync();

            var request = new GetSubscription(expectedSubscription.Id) { Id = expectedSubscription.Id };

            // Act
            var response = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Subscription);

            response.Subscription
                .Should()
                .BeEquivalentTo(
                    new GetSubscription.Response.SubscriptionDetail
                    {
                        Id = expectedSubscription.Id,
                        UserId = expectedSubscription.UserId,
                        CardNumber = expectedSubscription.CardNumber,
                        Valid = expectedSubscription.Valid,
                        SubscriptionType = expectedSubscription.SubscriptionType,
                        ExpirationDate = expectedSubscription.ExpirationDate
                    },
                    options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
