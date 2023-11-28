using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Booking;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Bookings;

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
                DatabaseName = "lacontessadb",
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
            }
        };

        _dbContext.Bookings.Add(existingBooking);

        _dbContext.SaveChangesAsync();

        // Seed the database with a user having the same email

        // Act & Assert
        Assert.ThrowsAsync<BookingAlreadyExistException>(() => _handler.Handle(createBookingRequest, new CancellationToken()));
    }
    
}