using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Booking;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Bookings;

[TestFixture]
public class UpdateBookingUnitTests : UnitTestBase
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

        _handler = new UpdateBookingHandler(_dbContext);
    }

    private UpdateBookingHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task Handle_WhenBookingExists_ShouldUpdateBooking()
    {
        var startingBooking = BookingTestFactory.Create();

        _dbContext.Bookings.Add(startingBooking);

        await _dbContext.SaveChangesAsync();

        var updatedBooking = BookingTestFactory.Create(x=>x.Id = startingBooking.Id);

        var UpdateBookingRequest = new UpdateBooking
        {
            Booking = new UpdateBooking.BookingDetail
            {
                Id = startingBooking.Id,
                UserId = updatedBooking.User.Id.ToString(),
                Date = updatedBooking.Date,
                ActivityId = updatedBooking.Activity.Id.ToString(),
                BookingName = updatedBooking.BookingName,
                PhoneNumber = updatedBooking.PhoneNumber,
                Price = updatedBooking.Price,
                IsLesson = updatedBooking.IsLesson,
                TimeSlot = updatedBooking.TimeSlot
            }
        };

        await _handler.Handle(UpdateBookingRequest, new CancellationToken());

        // Assert
        _dbContext.Bookings.ToList().Should().BeEquivalentTo(
            new[] { updatedBooking },
            options => options
                .ExcludingMissingMembers()
        );
    }

    [Test]
    public async Task Handle_WhenBookingNotFound_ShouldTBookingNotFoundException()
    {
        var startingBooking = BookingTestFactory.Create();

        _dbContext.Bookings.Add(startingBooking);

        await _dbContext.SaveChangesAsync();

        var updatedBooking = BookingTestFactory.Create();

        var UpdateBookingRequest = new UpdateBooking
        {
            Booking = new UpdateBooking.BookingDetail
            {
                Id = updatedBooking.Id,
                UserId = updatedBooking.User.Id.ToString(),
                Date = updatedBooking.Date,
                ActivityId = updatedBooking.Activity.Id.ToString(),
                BookingName = updatedBooking.BookingName,
                PhoneNumber = updatedBooking.PhoneNumber,
                Price = updatedBooking.Price,
                IsLesson = updatedBooking.IsLesson,
                TimeSlot = updatedBooking.TimeSlot
            }
        };

        // Assert
        Assert.ThrowsAsync<BookingNotFoundException>(() =>
            _handler.Handle(UpdateBookingRequest, new CancellationToken()));
    }
}