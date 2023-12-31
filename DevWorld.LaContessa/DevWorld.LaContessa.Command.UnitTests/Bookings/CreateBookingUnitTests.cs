//using DevWorld.LaContessa.Command.Abstractions.Bookings;
//using DevWorld.LaContessa.Command.Abstractions.Exceptions;
//using DevWorld.LaContessa.Command.Bookings;
//using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.TestUtils.TestFactories;
//using DevWorld.LaContessa.TestUtils.Utils;
//using FluentAssertions;

//namespace DevWorld.LaContessa.Command.UnitTests.Bookings;

//[Parallelizable(ParallelScope.None)]
//public class CreateBookingUnitTests : UnitTestBase
//{
//    private LaContessaDbContext _dbContext;
//    private CreateBookingHandler _handler;

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

//        _handler = new CreateBookingHandler(_dbContext);
//    }

//    [Test]
//    public async Task Handle_GivenNewBooking_ShouldCreateBooking()
//    {
//        // Arrange
//        var user = UserTestFactory.Create();
//        var activity = ActivityTestFactory.Create();

//        _dbContext.Users.Add(user);
//        _dbContext.Activities.Add(activity);

//        await _dbContext.SaveChangesAsync();

//        var testBooking = BookingTestFactory.Create( x =>
//        {
//            x.User = user;
//            x.Activity = activity;
//        });

//        var createBookingRequest = new CreateBooking
//        {
//            Bookings = new CreateBooking.BookingDetail
//            {
//                UserId = testBooking.User.Id.ToString(),
//                Date = testBooking.Date,
//                ActivityId = testBooking.Activity.Id.ToString(),
//                BookingName = testBooking.BookingName,
//                PhoneNumber = testBooking.PhoneNumber,
//                IsLesson = testBooking.IsLesson,
//                TimeSlot = testBooking.TimeSlot
//            }
//        };

//        // Act
//        await _handler.Handle(createBookingRequest, new CancellationToken());

//        // Assert
//        _dbContext.Bookings.ToList().Should().BeEquivalentTo(
//            new[] { testBooking },
//            options => options
//                .ExcludingMissingMembers()
//                .Excluding(x=>x.Id)
//        );
//    }

//    [Test]
//    public void Handle_GivenExistingBookingUserId_ShouldThrowBookingAlreadyExistException()
//    {
//        // Arrange
//        var existingBooking = BookingTestFactory.Create();
//        var createBookingRequest = new CreateBooking
//        {
//            Bookings = new CreateBooking.BookingDetail
//            {
//                UserId = existingBooking.User.Id.ToString(),
//                Date = existingBooking.Date,
//                ActivityId = existingBooking.Activity.Id.ToString(),
//                BookingName = existingBooking.BookingName,
//                PhoneNumber = existingBooking.PhoneNumber,
//                IsLesson = existingBooking.IsLesson,
//                TimeSlot = existingBooking.TimeSlot
//            }
//        };

//        _dbContext.Bookings.Add(existingBooking);

//        _dbContext.SaveChangesAsync();

//        // Seed the database with a user having the same email

//        // Act & Assert
//        Assert.ThrowsAsync<BookingAlreadyExistException>(() =>
//            _handler.Handle(createBookingRequest, new CancellationToken()));
//    }
//}