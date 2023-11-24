using NUnit.Framework;
using Moq;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using DevWorld.LaContessa.Domain.Entities.Bookings;
using DevWorld.LaContessa.Query.Abstractions;

namespace DevWorld.LaContessa.Tests
{
    [TestFixture]
    public class GetBookingsHandlerTests
    {
        private GetBookingsHandler _handler;
        private LaContessaDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Create options for the LaContessaDbContext
            var options = new LaContessaDbContextOptions
            {
                DatabaseName = "lacontessadb",
                ConnectionStringTemplate = "User ID=postgres;Password=lacontessa;Host=localhost;Port=5432;Database={0};Pooling=false;Timeout=120", // Adjust as needed
                CommandTimeout = 60, // Example timeout
                MigrationsAssembly = Assembly.GetAssembly(typeof(LaContessaDbContext))
            };

            // Initialize the DbContext with the options
            _dbContext = new LaContessaDbContext(options);

            // Seed the database with test data if necessary
              

            _handler = new GetBookingsHandler(_dbContext);
        }

        // Test methods go here
        [Test]
        public async Task GetBookingsHandler_ReturnsCorrectBookings()
        {
            _dbContext.Bookings.Add(new Booking { Id= Guid.NewGuid() });
            await _dbContext.SaveChangesAsync();
            
            // Arrange
            var request = new GetBookings();

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Bookings);
            // Further assertions can be made based on the expected test data
        }

    }
}