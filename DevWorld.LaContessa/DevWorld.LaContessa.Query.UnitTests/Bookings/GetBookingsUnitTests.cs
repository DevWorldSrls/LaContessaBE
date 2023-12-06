using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests
{
    [TestFixture]
    public class GetBookingsUnitTests : UnitTestBase
    {
        private GetBookingsHandler _handler;
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

            _handler = new GetBookingsHandler(_dbContext);
        }

        // Test methods go here
        [Test]
        public async Task GetBookingsHandler_ReturnsCorrectBookings()
        {

            // Arrange
            var expectedBooking = BookingTestFactory.Create();

            _dbContext.Bookings.Add(expectedBooking);

            await _dbContext.SaveChangesAsync();

            // Act
            var response = await _handler.Handle(new GetBookings(), CancellationToken);

            // Assert
            var bookingList = response.Bookings.ToList();

            Assert.IsNotNull(response);
            Assert.IsNotNull(bookingList);
            Assert.That(bookingList, Has.Count.EqualTo(1));

            bookingList
                .First()
                .Should()
                .BeEquivalentTo(
                    expectedBooking,
                    e => e.Excluding(x => x.DeletedAt)
                );
        }

    }
}