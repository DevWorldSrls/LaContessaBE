//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.Query.Abstractions.Bookings;
//using DevWorld.LaContessa.Query.Bookings;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;

//namespace DevWorld.LaContessa.Query.UnitTests.Bookings;

//[TestFixture]
//public class GetBookingHandlerTests : UnitTestBase
//{
//    [SetUp]
//    public void Setup()
//    {
//        _dbContext = new LaContessaDbContext(
//            new LaContessaDbContextOptions
//            {
//                DatabaseName = Guid.NewGuid().ToString(),
//                UseInMemoryProvider = true
//            });

//        _handler = new GetBookingHandler(_dbContext);
//    }

//    private GetBookingHandler _handler;
//    private LaContessaDbContext _dbContext;

//    [Test]
//    public async Task GetBookingHandler_ReturnsCorrectBooking()
//    {
//        // Arrange
//        var expectedBooking = BookingTestFactory.Create(); // Ensure this sets the Id property

//        _dbContext.Bookings.Add(expectedBooking);
//        await _dbContext.SaveChangesAsync();

//        var request = new GetBooking(expectedBooking.Id) { Id = expectedBooking.Id };

//        // Act
//        var response = await _handler.Handle(request, new CancellationToken());

//        // Assert
//        Assert.IsNotNull(response);
//        Assert.IsNotNull(response.Booking);

//        response.Booking
//            .Should()
//            .BeEquivalentTo(
//                new GetBooking.Response.BookingDetail
//                {
//                    Id = expectedBooking.Id,
//                    User = expectedBooking.User,
//                    Date = expectedBooking.Date,
//                    Activity = expectedBooking.Activity,
//                    TimeSlot = expectedBooking.TimeSlot,
//                    BookingName = expectedBooking.BookingName,
//                    PhoneNumber = expectedBooking.PhoneNumber,
//                    IsLesson = expectedBooking.IsLesson
//                },
//                options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
//            );
//    }
//}