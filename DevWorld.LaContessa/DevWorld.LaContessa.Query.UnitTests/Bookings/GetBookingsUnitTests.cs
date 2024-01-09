//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.Query.Abstractions.Bookings;
//using DevWorld.LaContessa.Query.Bookings;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;

//namespace DevWorld.LaContessa.Query.UnitTests.Bookings;

//[TestFixture]
//public class GetBookingsUnitTests : UnitTestBase
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

//        _handler = new GetBookingsHandler(_dbContext);
//    }

//    private GetBookingsHandler _handler;
//    private LaContessaDbContext _dbContext;

//    // Test methods go here
//    [Test]
//    public async Task GetBookingsHandler_ReturnsCorrectBookings()
//    {
//        // Arrange
//        var expectedBooking = BookingTestFactory.Create();

//        _dbContext.Bookings.Add(expectedBooking);

//        await _dbContext.SaveChangesAsync();

//        // Act
//        var response = await _handler.Handle(new GetBookings(), CancellationToken);

//        // Assert
//        var bookingList = response.Bookings.ToList();

//        Assert.IsNotNull(response);
//        Assert.IsNotNull(bookingList);
//        Assert.That(bookingList, Has.Count.EqualTo(1));

//        bookingList
//            .First()
//            .Should()
//            .BeEquivalentTo(
//                new GetBookings.Response.BookingDetail
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
//                options => options.ExcludingMissingMembers()
//            );
//    }
//}