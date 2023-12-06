using FluentAssertions;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetBookingByUserIdUnitTests : UnitTestBase
    {
        private GetBookingByUserIdHandler _handler;
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

            _handler = new GetBookingByUserIdHandler(_dbContext);
        }

        [Test]
        public async Task GetBookingByUserIdHandler_ReturnsCorrectBooking()
        {
            // Arrange
            var expectedBooking = BookingTestFactory.Create(); // Ensure this sets the Id property

            _dbContext.Bookings.Add(expectedBooking);
            await _dbContext.SaveChangesAsync();

            var request = new GetBookingByUserId(expectedBooking.UserId) { UserId = expectedBooking.UserId };

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
