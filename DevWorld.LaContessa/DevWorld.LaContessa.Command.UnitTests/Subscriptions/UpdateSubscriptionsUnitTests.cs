using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Command.Subscription;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Subscriptions
{
    [TestFixture]
    public class UpdateSubscriptionsUnitTests : UnitTestBase
    {
        private UpdateSubscriptionHandler _handler;
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

            _handler = new UpdateSubscriptionHandler(_dbContext);
        }

        [Test]
        public async Task Handle_WhenSubscriptionExists_ShouldUpdateSubscription()
        {
            var startingSubscription = SubscriptionTestFactory.Create();

            _dbContext.Subscriptions.Add(startingSubscription);

            _dbContext.SaveChangesAsync();

            var updatedSubscription = SubscriptionTestFactory.Create();

            var UpdateSubscriptionRequest = new UpdateSbscription
            {
                Subscription = new UpdateSbscription.SubscriptionDetail()
                {
                    Id = startingSubscription.Id,
                    UserId = updatedSubscription.UserId,
                    Number = updatedSubscription.Number,
                    Valid = updatedSubscription.Valid
                }
            };

            await _handler.Handle(UpdateSubscriptionRequest, new CancellationToken());

            // Assert
            _dbContext.Subscriptions.ToList().Should().BeEquivalentTo(
                new[] {updatedSubscription},
                options => options
                    .Including(x => x.UserId)
                    .Including(x => x.Number)
                    .Including(x => x.Valid)
                    .ExcludingMissingMembers() 
            );
        }

        [Test]
        public async Task Handle_WhenSubscriptionNotFound_ShouldThrowSubscriptionNotFoundException()
        {
            var startingSubscription = SubscriptionTestFactory.Create();

            _dbContext.Subscriptions.Add(startingSubscription);

            _dbContext.SaveChangesAsync();

            var updatedSubscription = SubscriptionTestFactory.Create();

            var UpdateSubscriptionRequest = new UpdateSbscription
            {
                Subscription = new UpdateSbscription.SubscriptionDetail()
                {
                    Id = updatedSubscription.Id,
                    UserId = updatedSubscription.UserId,
                    Number = updatedSubscription.Number,
                    Valid = updatedSubscription.Valid
                }
            };

            // Assert
            Assert.ThrowsAsync<SubscriptionNotFoundException>(() => _handler.Handle(UpdateSubscriptionRequest, new CancellationToken()));
            
        }
    }
}
