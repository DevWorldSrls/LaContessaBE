using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Command.Subscription;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Subscriptions;

[TestFixture]
public class UpdateSubscriptionsUnitTests : UnitTestBase
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

        _handler = new UpdateSubscriptionHandler(_dbContext);
    }

    private UpdateSubscriptionHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task Handle_WhenSubscriptionExists_ShouldUpdateSubscription()
    {
        var user = UserTestFactory.Create();
        var activity = ActivityTestFactory.Create();
        var startingSubscription = SubscriptionTestFactory.Create();

        _dbContext.Users.Add(user);
        _dbContext.Activities.Add(activity);
        _dbContext.Subscriptions.Add(startingSubscription);

        await _dbContext.SaveChangesAsync();

        var updatedSubscription = SubscriptionTestFactory.Create(x =>
        {
            x.Id = startingSubscription.Id;
            x.User = user;
            x.Activity = activity;
        });

        var UpdateSubscriptionRequest = new UpdateSbscription
        {
            Subscription = new UpdateSbscription.SubscriptionDetail
            {
                Id = startingSubscription.Id,
                UserId = user.Id.ToString(),
                ActivityId = activity.Id.ToString(),
                CardNumber = updatedSubscription.CardNumber,
                Valid = updatedSubscription.Valid,
                ExpirationDate = updatedSubscription.ExpirationDate,
                SubscriptionType = updatedSubscription.SubscriptionType
            }
        };

        await _handler.Handle(UpdateSubscriptionRequest, new CancellationToken());

        // Assert
        _dbContext.Subscriptions.ToList().Should().BeEquivalentTo(
            new[] { updatedSubscription },
            options => options
                .Including(x => x.User)
                .Including(x => x.Activity)
                .Including(x => x.CardNumber)
                .Including(x => x.Valid)
                .Including(x => x.ExpirationDate)
                .Including(x => x.SubscriptionType)
                .ExcludingMissingMembers()
        );
    }

    [Test]
    public async Task Handle_WhenSubscriptionNotFound_ShouldThrowSubscriptionNotFoundException()
    {
        var startingSubscription = SubscriptionTestFactory.Create();

        _dbContext.Subscriptions.Add(startingSubscription);

        await _dbContext.SaveChangesAsync();

        var updatedSubscription = SubscriptionTestFactory.Create();

        var UpdateSubscriptionRequest = new UpdateSbscription
        {
            Subscription = new UpdateSbscription.SubscriptionDetail
            {
                Id = updatedSubscription.Id,
                UserId = updatedSubscription.User.Id.ToString(),
                CardNumber = updatedSubscription.CardNumber,
                Valid = updatedSubscription.Valid,
                ExpirationDate = updatedSubscription.ExpirationDate,
                SubscriptionType = updatedSubscription.SubscriptionType
            }
        };

        // Assert
        Assert.ThrowsAsync<SubscriptionNotFoundException>(() =>
            _handler.Handle(UpdateSubscriptionRequest, new CancellationToken()));
    }
}