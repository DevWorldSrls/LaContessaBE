//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
//using DevWorld.LaContessa.Query.Subscriptions;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;

//namespace DevWorld.LaContessa.Query.UnitTests.Subscriptions;

//[TestFixture]
//public class GetSubscriptionUnitTests : UnitTestBase
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

//        _handler = new GetSubscriptionHandler(_dbContext);
//    }

//    private GetSubscriptionHandler _handler;
//    private LaContessaDbContext _dbContext;

//    [Test]
//    public async Task GetSubscriptionHandler_ReturnsCorrectSubscription()
//    {
//        // Arrange
//        var expectedSubscription = SubscriptionTestFactory.Create();

//        _dbContext.Subscriptions.Add(expectedSubscription);
//        await _dbContext.SaveChangesAsync();

//        var request = new GetSubscription(expectedSubscription.Id) { Id = expectedSubscription.Id };

//        // Act
//        var response = await _handler.Handle(request, new CancellationToken());

//        // Assert
//        Assert.IsNotNull(response);
//        Assert.IsNotNull(response.Subscription);

//        response.Subscription
//            .Should()
//            .BeEquivalentTo(
//                new GetSubscription.Response.SubscriptionDetail
//                {
//                    Id = expectedSubscription.Id,
//                    User = expectedSubscription.User,
//                    Activity = expectedSubscription.Activity,
//                    CardNumber = expectedSubscription.CardNumber,
//                    Valid = expectedSubscription.Valid,
//                    SubscriptionType = expectedSubscription.SubscriptionType,
//                    ExpirationDate = expectedSubscription.ExpirationDate
//                },
//                options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
//            );
//    }
//}