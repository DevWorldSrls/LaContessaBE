using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Activities;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Command.UnitTests.Activities;

public class CreateActivityUnitTests : UnitTestBase
{
    private LaContessaDbContext _dbContext;
    private CreateActivityHandler _handler;

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

        _handler = new CreateActivityHandler(_dbContext);
    }

    [Test]
    public async Task Handle_GivenNewActivity_ShouldCreateActivity()
    {
        var testActivity = ActivityTestFactory.Create();

        // Arrange
        var createActivityRequest = new CreateActivity
        {
            Activity = new CreateActivity.ActivityDetail
            {
                Name = testActivity.Name,
                IsOutdoor = testActivity.IsOutdoor,
                IsSubscriptionRequired = testActivity.IsSubscriptionRequired,
                Description = testActivity.Description,
                ActivityImg = testActivity.ActivityImg,
                ServiceList = testActivity.ServiceList.Select(domainService => new CreateActivity.Service
                {
                    Icon = domainService.Icon,
                    ServiceName = domainService.ServiceName
                }).ToList(),
                DateList = testActivity.DateList.Select(domainDate => new CreateActivity.ActivityDate
                {
                    Date = domainDate.Date,
                    TimeSlotList = domainDate.TimeSlotList.Select(domainTimeSlot => new CreateActivity.ActivityTimeSlot
                    {
                        TimeSlot = domainTimeSlot.TimeSlot,
                        IsAlreadyBooked = domainTimeSlot.IsAlreadyBooked
                    }).ToList()
                }).ToList()
            }
        };

        // Act
        await _handler.Handle(createActivityRequest, new CancellationToken());

        // Assert
        _dbContext.Activities.ToList().Should().BeEquivalentTo(
            new[] { testActivity },
            options => options
                .ExcludingMissingMembers()
                .Excluding(x => x.Id)
        );
    }

    [Test]
    public void Handle_GivenExistingBookingUserId_ShouldThrowBookingAlreadyExistException()
    {
        // Arrange
        var ExistingActivity = ActivityTestFactory.Create();

        var createActivityRequest = new CreateActivity
        {
            Activity = new CreateActivity.ActivityDetail
            {
                Name = ExistingActivity.Name,
                IsOutdoor = ExistingActivity.IsOutdoor,
                Description = ExistingActivity.Description,
                ActivityImg = ExistingActivity.ActivityImg,
                ServiceList = ExistingActivity.ServiceList.Select(domainService => new CreateActivity.Service
                {
                    Icon = domainService.Icon,
                    ServiceName = domainService.ServiceName
                }).ToList(),
                DateList = ExistingActivity.DateList.Select(domainDate => new CreateActivity.ActivityDate
                {
                    Date = domainDate.Date,
                    TimeSlotList = domainDate.TimeSlotList.Select(domainTimeSlot => new CreateActivity.ActivityTimeSlot
                    {
                        TimeSlot = domainTimeSlot.TimeSlot,
                        IsAlreadyBooked = domainTimeSlot.IsAlreadyBooked
                    }).ToList()
                }).ToList()
            }
        };

        _dbContext.Activities.Add(ExistingActivity);

        _dbContext.SaveChangesAsync();

        // Seed the database with a activity having the same name

        // Act & Assert
        Assert.ThrowsAsync<ActivityAlreadyExistException>(() =>
            _handler.Handle(createActivityRequest, new CancellationToken()));
    }
}