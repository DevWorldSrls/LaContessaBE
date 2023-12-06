using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Booking;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Bookings;

[Parallelizable(ParallelScope.None)]
public class CreateBookingUnitTests : UnitTestBase
{
    private CreateBookingHandler _handler;
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

        _handler = new CreateBookingHandler(_dbContext);
    }

    [Test]
    public async Task Handle_GivenNewBooking_ShouldCreateBooking()
    {
        var testBooking = BookingTestFactory.Create(); 
            
        // Arrange
        var createBookingRequest = new CreateBooking()
        {
            Booking = new CreateBooking.BookingDetail
            {
                UserId = testBooking.UserId,
                Date = testBooking.Date,
                activityID = testBooking.activityID,
                bookingName = testBooking.bookingName,
                phoneNumber = testBooking.phoneNumber,
                price = testBooking.price,
                IsLesson = testBooking.IsLesson,
                timeSlot = testBooking.timeSlot
            }
        };

        // Act
        await _handler.Handle(createBookingRequest, new CancellationToken());

        // Assert
        _dbContext.Bookings.ToList().Should().BeEquivalentTo(
            new[] {testBooking},
            options => options
                .Including(x => x.UserId)
                .Including(x => x.Date)
                .Including(x => x.activityID)
                .Including(x => x.bookingName)
                .Including(x => x.bookingName)
                .Including(x => x.phoneNumber)
                .Including(x => x.price)
                .Including(x => x.IsLesson)
                .Including(x => x.timeSlot)
                .ExcludingMissingMembers() 
        );
    }
    
    [Test]
    public void Handle_GivenExistingBookingUserId_ShouldThrowBookingAlreadyExistException()
    {
        // Arrange
        var existingBooking = BookingTestFactory.Create();
        var createBookingRequest = new CreateBooking()
        {
            Booking = new CreateBooking.BookingDetail
            {
                UserId = existingBooking.UserId,
                Date = existingBooking.Date,
                activityID = existingBooking.activityID,
                bookingName = existingBooking.bookingName,
                phoneNumber = existingBooking.phoneNumber,
                price = existingBooking.price,
                IsLesson = existingBooking.IsLesson,
                timeSlot = existingBooking.timeSlot
            }
        };

        _dbContext.Bookings.Add(existingBooking);

        _dbContext.SaveChangesAsync();

        // Seed the database with a user having the same email

        // Act & Assert
        Assert.ThrowsAsync<BookingAlreadyExistException>(() => _handler.Handle(createBookingRequest, new CancellationToken()));
    }
    
}