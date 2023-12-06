using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Booking;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Bookings
{
    [TestFixture]
    public class UpdateBookingUnitTests : UnitTestBase
    {
        private UpdateBookingHandler _handler;
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

            _handler = new UpdateBookingHandler(_dbContext);
        }

        [Test]
        public async Task Handle_WhenBookingExists_ShouldUpdateBooking()
        {
            var startingBooking = BookingTestFactory.Create();

            _dbContext.Bookings.Add(startingBooking);

            _dbContext.SaveChangesAsync();

            var updatedBooking = BookingTestFactory.Create();

            var UpdateBookingRequest = new UpdateBooking
            {
                Booking = new UpdateBooking.BookingDetail()
                {
                    Id = startingBooking.Id,
                    UserId = updatedBooking.UserId,
                    Date = updatedBooking.Date,
                    activityID = updatedBooking.activityID,
                    bookingName = updatedBooking.bookingName,
                    phoneNumber = updatedBooking.phoneNumber,
                    price = updatedBooking.price,
                    IsLesson = updatedBooking.IsLesson
                }
            };

            await _handler.Handle(UpdateBookingRequest, new CancellationToken());

            // Assert
            _dbContext.Bookings.ToList().Should().BeEquivalentTo(
                new[] {updatedBooking},
                options => options
                    .Including(x => x.UserId)
                    .Including(x => x.Date)
                    .Including(x => x.activityID)
                    .Including(x => x.bookingName)
                    .Including(x => x.bookingName)
                    .Including(x => x.phoneNumber)
                    .Including(x => x.price)
                    .Including(x => x.IsLesson)
                    .ExcludingMissingMembers() 
            );
        }

        [Test]
        public async Task Handle_WhenBookingNotFound_ShouldTBookingNotFoundException()
        {
            var startingBooking = BookingTestFactory.Create();

            _dbContext.Bookings.Add(startingBooking);

            _dbContext.SaveChangesAsync();

            var updatedBooking = BookingTestFactory.Create();

            var UpdateBookingRequest = new UpdateBooking
            {
                Booking = new UpdateBooking.BookingDetail()
                {
                    Id = updatedBooking.Id,
                    UserId = updatedBooking.UserId,
                    Date = updatedBooking.Date,
                    activityID = updatedBooking.activityID,
                    bookingName = updatedBooking.bookingName,
                    phoneNumber = updatedBooking.phoneNumber,
                    price = updatedBooking.price,
                    IsLesson = updatedBooking.IsLesson
                }
            };

            // Assert
            Assert.ThrowsAsync<BookingNotFoundException>(() => _handler.Handle(UpdateBookingRequest, new CancellationToken()));
            
        }
    }
}
