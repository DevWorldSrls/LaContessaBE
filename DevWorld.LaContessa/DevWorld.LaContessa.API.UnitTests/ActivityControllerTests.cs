using Moq;
using MediatR;
using DevWorld.LaContessa.API.Controllers;
using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Query.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.Tests
{
    [TestFixture]
    public class ActivityControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private ActivityController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ActivityController(_mockMediator.Object);
        }

        [Test]
        public async Task GetActivities_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResponse = new GetActivities.Response()
            {
                Activities = new[]
                {
                    new GetActivities.Response.ActivityDetail() { Id = Guid.NewGuid() },
                    new GetActivities.Response.ActivityDetail() { Id = Guid.NewGuid() }
                }
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetActivities>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetActivities(CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<GetActivities.Response>>(result);
            Assert.AreEqual(expectedResponse, result.Value);
        }
        
        [Test]
        public async Task GetActivity_ReturnsExpectedResult()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var expectedResponse = new GetActivity.Response()
            {
                Activity = new GetActivity.Response.ActivityDetail() { Id = expectedId }
            };
            _mockMediator.Setup(m => m.Send(It.Is<GetActivity>(cmd => cmd.Id == expectedId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetActivity(expectedId, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<GetActivity.Response>>(result);
            Assert.AreEqual(expectedResponse, result.Value);
        }
        
        [Test]
        public async Task CreateActivity_ReturnsOkResult()
        {
            // Arrange
            var activityDetail = new CreateActivity.ActivityDetail();
            _mockMediator.Setup(m => m.Send(It.Is<CreateActivity>(cmd => cmd.Activity == activityDetail), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateActivity(activityDetail, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateActivity_ReturnsOkResult()
        {
            // Arrange
            var activityDetail = new UpdateActivity.ActivityDetail();
            _mockMediator.Setup(m => m.Send(It.Is<UpdateActivity>(cmd => cmd.Activity == activityDetail), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateActivity(activityDetail, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }


    }
}