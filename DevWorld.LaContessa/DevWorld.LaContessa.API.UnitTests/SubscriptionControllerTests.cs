using Moq;
using MediatR;
using DevWorld.LaContessa.API.Controllers;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Query.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.Tests
{
    [TestFixture]
    public class SubscriptionControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private SubscriptionController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new SubscriptionController(_mockMediator.Object);
        }
        
        [Test]
        public async Task GetSubscriptions_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResponse = new GetSubscriptions.Response()
            {
                Subscriptions = new []
                {
                    new GetSubscriptions.Response.SubscriptionDetail()
                    {
                        Id = Guid.NewGuid()
                    },
                    new GetSubscriptions.Response.SubscriptionDetail()
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
            
            Assert.AreEqual(expectedResponse, result.Value);
        }

        [Test]
        public async Task GetSubscription_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResponse = new GetSubscription.Response()
            {
                Subscription = 
                    new GetSubscription.Response.SubscriptionDetail()
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
            Assert.IsInstanceOf<ActionResult<GetSubscriptions.Response>>(result);
            
            Assert.AreEqual(expectedResponse, result.Value);
        }
        
        [Test]
        public async Task CreateSubscription_ReturnsOkResult()
        {
            // Arrange
            var subscriptionDetail = new CreateSubscription.SubscriptionDetail();
            _mockMediator.Setup(m => m.Send(It.Is<CreateSubscription>(cmd => cmd.Subscription == subscriptionDetail), It.IsAny<CancellationToken>()))
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
            _mockMediator.Setup(m => m.Send(It.Is<UpdateSbscription>(cmd => cmd.Subscription == subscriptionDetail), It.IsAny<CancellationToken>()))
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
            _mockMediator.Setup(m => m.Send(It.Is<GetSubscriptionByUserId>(cmd => cmd.UserId == userId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetSubscription(userId, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<GetSubscriptionByUserId.Response>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(expectedResponse, actionResult.Value);
        }
    }
}