//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
//using DevWorld.LaContessa.Query.Subscriptions;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;

//namespace DevWorld.LaContessa.Query.UnitTests.Subscriptions;

//[TestFixture]
//public class GetSubscriptionsUnitTests : UnitTestBase
//{
//    [SetUp]
//    public void Setup()
//    {
//        // Initialize the in-memory database context and the handler
//        _dbContext = new LaContessaDbContext(
//            new LaContessaDbContextOptions
//            {
//                DatabaseName = Guid.NewGuid().ToString(),
//                UseInMemoryProvider = true
//            });

//        _handler = new GetSubscriptionsHandler(_dbContext);
//    }

//    private GetSubscriptionsHandler _handler;
//    private LaContessaDbContext _dbContext;

//    [Test]
//    public async Task GetSubscriptionsHandler_ReturnsCorrectSubscriptions()
//    {
//        // Arrange
//        var expectedSubscription1 = SubscriptionTestFactory.Create();
//        var expectedSubscription2 = SubscriptionTestFactory.Create();

//        _dbContext.Subscriptions.Add(expectedSubscription1);
//        _dbContext.Subscriptions.Add(expectedSubscription2);
//        await _dbContext.SaveChangesAsync();

//        // Act
//        var response = await _handler.Handle(new GetSubscriptions(), new CancellationToken());

//        // Assert
//        Assert.IsNotNull(response);
//        Assert.IsNotNull(response.Subscriptions);

//        response.Subscriptions
//            .Should()
//            .BeEquivalentTo(
//                new[] { expectedSubscription1, expectedSubscription2 },
//                options => options.Including(x => x.Id)
//                    .Including(x => x.User)
//                    .Including(x => x.CardNumber)
//                    .Including(x => x.Valid)
//                    .Including(x => x.ExpirationDate)
//                    .Including(x => x.SubscriptionType)
//                    .ExcludingMissingMembers() // Exclude fields that are not part of the response
//            );
//    }
//}