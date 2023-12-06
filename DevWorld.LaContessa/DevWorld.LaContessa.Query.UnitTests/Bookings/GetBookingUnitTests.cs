using FluentAssertions;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetBookingHandlerTests : UnitTestBase
    {
        private GetBookingHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new LaContessaDbContext(
                new LaContessaDbContextOptions
                {
                    DatabaseName = Guid.NewGuid().ToString(),
                    UseInMemoryProvider = true,
                });

            _handler = new GetBookingHandler(_dbContext);
        }

        [Test]
        public async Task GetBookingHandler_ReturnsCorrectBooking()
        {
            // Arrange
            var expectedBooking = BookingTestFactory.Create(); // Ensure this sets the Id property

            _dbContext.Bookings.Add(expectedBooking);
            await _dbContext.SaveChangesAsync();

            var request = new GetBooking(expectedBooking.Id) { Id = expectedBooking.Id };

            // Act
            var response = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Booking);

            response.Booking
                .Should()
                .BeEquivalentTo(
                    new GetBooking.Response.BookingDetail
                    {
                        Id = expectedBooking.Id,
                        UserId = expectedBooking.UserId,
                        Date = expectedBooking.Date,
                        activityID = expectedBooking.activityID,
                        timeSlot = expectedBooking.timeSlot,
                        bookingName = expectedBooking.bookingName,
                        phoneNumber = expectedBooking.phoneNumber,
                        price = expectedBooking.price,
                        IsLesson = expectedBooking.IsLesson
                    },
                    options => options.ExcludingMissingMembers() // Exclude fields that are not part of the response
                );
        }
    }
}
