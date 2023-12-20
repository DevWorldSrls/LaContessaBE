using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activity;
using DevWorld.LaContessa.TestUtils.TestFactories;
using DevWorld.LaContessa.TestUtils.Utils;
using FluentAssertions;

namespace DevWorld.LaContessa.Query.UnitTests;

[TestFixture]
public class GetActivityHandlerTests : UnitTestBase
{
    [SetUp]
    public void Setup()
    {
        _dbContext = new LaContessaDbContext(
            new LaContessaDbContextOptions
            {
                DatabaseName = Guid.NewGuid().ToString(),
                UseInMemoryProvider = true
            });

        _handler = new GetActivityHandler(_dbContext);
    }

    private GetActivityHandler _handler;
    private LaContessaDbContext _dbContext;

    [Test]
    public async Task GetActivityHandler_ReturnsCorrectBooking()
    {
        // Arrange
        var expectedActivity = ActivityTestFactory.Create();

        _dbContext.Activities.Add(expectedActivity);
        await _dbContext.SaveChangesAsync();

        var request = new GetActivity(expectedActivity.Id) { Id = expectedActivity.Id };

        // Act
        var response = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Activity);

        response.Activity
            .Should()
            .BeEquivalentTo(
                new GetActivity.Response.ActivityDetail
                {
                    Id = expectedActivity.Id,
                    Name = expectedActivity.Name,
                    IsOutdoor = expectedActivity.IsOutdoor,
                    IsSubscriptionRequired = expectedActivity.IsSubscriptionRequired,
                    Description = expectedActivity.Description,
                    ActivityImg = expectedActivity.ActivityImg,
                    ServiceList = expectedActivity.ServiceList.Select(domainService => new GetActivity.Response.Service
                    {
                        Icon = domainService.Icon,
                        ServiceName = domainService.ServiceName
                    }).ToList(),
                    DateList = expectedActivity.DateList.Select(domainDate => new GetActivity.Response.ActivityDate
                    {
                        Date = domainDate.Date,
                        TimeSlotList = domainDate.TimeSlotList.Select(domainTimeSlot =>
                            new GetActivity.Response.ActivityTimeSlot
                            {
                                TimeSlot = domainTimeSlot.TimeSlot,
                                IsAlreadyBooked = domainTimeSlot.IsAlreadyBooked
                            }).ToList()
                    }).ToList()
                },
                config => config.ExcludingMissingMembers() // Exclude fields that are not part of the response
            );
    }
}