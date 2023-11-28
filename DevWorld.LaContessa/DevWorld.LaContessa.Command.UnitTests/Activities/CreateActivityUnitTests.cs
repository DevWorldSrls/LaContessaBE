using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Activity;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Activities;

public class CreateActivityUnitTests : UnitTestBase
{
    private CreateActivityHandler _handler;
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

        _handler = new CreateActivityHandler(_dbContext);
    }

    [Test]
    public async Task Handle_GivenNewActivity_ShouldCreateActivity()
    {
        var testActivity = ActivityTestFactory.Create(); 
            
        // Arrange
        var createActivityRequest = new CreateActivity()
        {
            Activity = new CreateActivity.ActivityDetail
            {
                Name = testActivity.Name,
                Type = testActivity.Type,
                IsAvaible = testActivity.IsAvaible,
                Descripting = testActivity.Description,
                Services = testActivity.Services,
                Dates = testActivity.Dates
            }
        };

        // Act
        await _handler.Handle(createActivityRequest, new CancellationToken());

        // Assert
        _dbContext.Activities.ToList().Should().BeEquivalentTo(
            new[] {testActivity},
            options => options
                .Including(x => x.Name)
                .Including(x => x.Type)
                .Including(x => x.IsAvaible)
                .Including(x => x.Description)
                .Including(x => x.Services)
                .Including(x => x.Dates)
                .ExcludingMissingMembers() 
        );
    }
    
    [Test]
    public void Handle_GivenExistingBookingUserId_ShouldThrowBookingAlreadyExistException()
    {
        // Arrange
        var ExistingActivity = ActivityTestFactory.Create(); 
            
        var createActivityRequest = new CreateActivity()
        {
            Activity = new CreateActivity.ActivityDetail
            {
                Name = ExistingActivity.Name,
                Type = ExistingActivity.Type,
                IsAvaible = ExistingActivity.IsAvaible,
                Descripting = ExistingActivity.Description,
                Services = ExistingActivity.Services,
                Dates = ExistingActivity.Dates
            }
        };

        _dbContext.Activities.Add(ExistingActivity);

        _dbContext.SaveChangesAsync();

        // Seed the database with a activity having the same name

        // Act & Assert
        Assert.ThrowsAsync<ActivityAlreadyExistException>(() => _handler.Handle(createActivityRequest, new CancellationToken()));
    }
    
}