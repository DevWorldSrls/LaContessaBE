using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Command.Subscription;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Subscriptions;

public class CreateSubscriptionsUnitTests : UnitTestBase
{
    private CreateSubscriptionHandler _handler;
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

        _handler = new CreateSubscriptionHandler(_dbContext);
    }

    [Test]
    public async Task Handle_GivenNewSubscription_ShouldCreateSubscription()
    {
        var testSubscription = SubscriptionTestFactory.Create(); 
            
        // Arrange
        var createSubscriptionRequest = new CreateSubscription
        {
            Subscription = new CreateSubscription.SubscriptionDetail()
            {
                UserId = testSubscription.UserId,
                CardNumber = testSubscription.CardNumber,
                Valid = testSubscription.Valid,
                ExpirationDate = testSubscription.ExpirationDate,
                SubscriptionType = testSubscription.SubscriptionType
            }
        };

        // Act
        await _handler.Handle(createSubscriptionRequest, new CancellationToken());

        // Assert
        _dbContext.Subscriptions.ToList().Should().BeEquivalentTo(
            new[] {testSubscription},
            options => options
                .Including(x => x.UserId)
                .Including(x => x.CardNumber)
                .Including(x => x.Valid)
                .Including(x => x.ExpirationDate)
                .Including(x => x.SubscriptionType)
                .ExcludingMissingMembers() 
        );
    }
    
    [Test]
    public void Handle_GivenExistingUserEmail_ShouldThrowSubscriptionAlreadyExistException()
    {
        // Arrange
        var existingSubscription = SubscriptionTestFactory.Create();
        var createSubscriptionRequest = new CreateSubscription
        {
            Subscription = new CreateSubscription.SubscriptionDetail()
            {
                UserId = existingSubscription.UserId,
                CardNumber = existingSubscription.CardNumber,
                Valid = existingSubscription.Valid,
                ExpirationDate = existingSubscription.ExpirationDate,
                SubscriptionType = existingSubscription.SubscriptionType
            }
        };

        _dbContext.Subscriptions.Add(existingSubscription);

        _dbContext.SaveChangesAsync();

        // Seed the database with a user having the same email

        // Act & Assert
        Assert.ThrowsAsync<SubscriptionAlreadyExistException>(() => _handler.Handle(createSubscriptionRequest, new CancellationToken()));
    }
    
}