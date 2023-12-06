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
    public class UpdateActivityUnitTests : UnitTestBase
    {
        private UpdateActivityHandler _handler;
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

            _handler = new UpdateActivityHandler(_dbContext);
        }

        [Test]
        public async Task Handle_WhenActivityExists_ShouldUpdateActivity()
        {
            var startingActivity = ActivityTestFactory.Create();

            _dbContext.Activities.Add(startingActivity);

            _dbContext.SaveChangesAsync();

            var updatedActivity = ActivityTestFactory.Create();

            var UpdateActivityRequest = new UpdateActivity
            {
                Activity = new UpdateActivity.ActivityDetail
                {
                    Id = startingActivity.Id,
                    Name = updatedActivity.Name,
                    IsOutdoor = updatedActivity.IsOutdoor,
                    Description = updatedActivity.Description,
                    ActivityImg = updatedActivity.ActivityImg,
                    ServiceList = updatedActivity.ServiceList.Select(domainService => new UpdateActivity.Service
                    {
                        Icon = domainService.Icon,
                        ServiceName = domainService.ServiceName,
                    }).ToList(),
                    DateList = updatedActivity.DateList.Select(domainDate => new UpdateActivity.ActivityDate
                    {
                        Date = domainDate.Date,
                        TimeSlotList = domainDate.TimeSlotList.Select(domainTimeSlot => new UpdateActivity.ActivityTimeSlot
                        {
                            TimeSlot = domainTimeSlot.TimeSlot,
                            IsAlreadyBooked = domainTimeSlot.IsAlreadyBooked
                        }).ToList()
                    }).ToList(),
                }
            };

            await _handler.Handle(UpdateActivityRequest, new CancellationToken());

            // Assert
            _dbContext.Activities.ToList().Should().BeEquivalentTo(
                new[] {updatedActivity},
                options => options
                    .Including(x => x.Name)
                    .Including(x => x.IsOutdoor)
                    .Including(x => x.Description)
                    .Including(x => x.ActivityImg)
                    .Including(x => x.ServiceList)
                    .Including(x => x.DateList)
                    .ExcludingMissingMembers() 
            );
        }

        [Test]
        public async Task Handle_WhenActivityNotFound_ShouldTActivityNotFoundException()
        {
            var startingActivity = ActivityTestFactory.Create();

            _dbContext.Activities.Add(startingActivity);

            _dbContext.SaveChangesAsync();

            var updatedActivity = ActivityTestFactory.Create();

            var UpdateActivityRequest = new UpdateActivity
            {
                Activity = new UpdateActivity.ActivityDetail
                {
                    Id = updatedActivity.Id,
                    Name = updatedActivity.Name,
                    Description = updatedActivity.Description,
                    IsOutdoor = updatedActivity.IsOutdoor,
                    ActivityImg = updatedActivity.ActivityImg,
                    ServiceList = updatedActivity.ServiceList.Select(domainService => new UpdateActivity.Service
                    {
                        Icon = domainService.Icon,
                        ServiceName = domainService.ServiceName,
                    }).ToList(),
                    DateList = updatedActivity.DateList.Select(domainDate => new UpdateActivity.ActivityDate
                    {
                        Date = domainDate.Date,
                        TimeSlotList = domainDate.TimeSlotList.Select(domainTimeSlot => new UpdateActivity.ActivityTimeSlot
                        {
                            TimeSlot = domainTimeSlot.TimeSlot,
                            IsAlreadyBooked = domainTimeSlot.IsAlreadyBooked
                        }).ToList()
                    }).ToList(),
                }
            };

            // Assert
            Assert.ThrowsAsync<ActivityNotFoundException>(() => _handler.Handle(UpdateActivityRequest, new CancellationToken()));
            
        }
    }
}
