using DevWorld.LaContessa.API.Controllers;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DevWorld.LaContessa.API.UnitTests;

[TestFixture]
public class SubscriptionControllerTests
{
    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new SubscriptionController(_mockMediator.Object);
    }

    private Mock<IMediator> _mockMediator;
    private SubscriptionController _controller;

    [Test]
    public async Task GetSubscriptions_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new GetSubscriptions.Response
        {
            Subscriptions = new[]
            {
                new GetSubscriptions.Response.SubscriptionDetail
                {
                    Id = Guid.NewGuid()
                },
                new GetSubscriptions.Response.SubscriptionDetail
                {
                    Id = Guid.NewGuid()
                }
            }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetSubscriptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetSubscriptions(CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ActionResult<GetSubscriptions.Response>>(result);

        Assert.That(result.Value, Is.EqualTo(expectedResponse));
    }

    [Test]
    public async Task GetSubscription_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResponse = new GetSubscription.Response
        {
            Subscription =
                new GetSubscription.Response.SubscriptionDetail
                {
                    Id = Guid.NewGuid()
                }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetSubscription>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetSubscription(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ActionResult<GetSubscription.Response>>(result);

        Assert.That(result.Value, Is.EqualTo(expectedResponse));
    }

    [Test]
    public async Task CreateSubscription_ReturnsOkResult()
    {
        // Arrange
        var subscriptionDetail = new CreateSubscription.SubscriptionDetail();
        _mockMediator.Setup(m => m.Send(It.Is<CreateSubscription>(cmd => cmd.Subscription == subscriptionDetail),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateSubscription(subscriptionDetail, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task UpdateSubscription_ReturnsOkResult()
    {
        // Arrange
        var subscriptionDetail = new UpdateSbscription.SubscriptionDetail();
        _mockMediator.Setup(m => m.Send(It.Is<UpdateSbscription>(cmd => cmd.Subscription == subscriptionDetail),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateSubscription(subscriptionDetail, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task GetSubscriptionByUserId_ReturnsExpectedResult()
    {
        // Arrange
        var userId = "someUserId";
        var expectedResponse = new GetSubscriptionByUserId.Response();
        _mockMediator.Setup(m =>
                m.Send(It.Is<GetSubscriptionByUserId>(cmd => cmd.UserId == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetSubscription(userId, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ActionResult<GetSubscriptionByUserId.Response>>(result);
        Assert.That(result.Value, Is.EqualTo(expectedResponse));
    }
}