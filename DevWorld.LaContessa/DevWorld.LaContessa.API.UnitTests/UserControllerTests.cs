using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevWorld.LaContessa.API.Controllers;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Query.Abstractions;

namespace DevWorld.LaContessa.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new UserController(_mockMediator.Object);
        }

        [Test]
        public async Task GetUsers_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResponse = new GetUsers.Response()
            {
                Users = new []
                {
                    new GetUsers.Response.UserDetail()
                    {
                        Id = Guid.NewGuid()
                    },new GetUsers.Response.UserDetail()
                    {
                        Id = Guid.NewGuid()
                    }
                }
            }; // Properly initialized

            _mockMediator.Setup(m => m.Send(It.IsAny<GetUsers>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetUsers(CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            
            Assert.IsInstanceOf<GetUsers.Response>(result.Value);
            Assert.AreEqual(expectedResponse, result.Value);
        }
        
        [Test]
        public async Task GetUser_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResponse = new GetUser.Response()
            {
                User = new GetUser.Response.UserDetail()
                {
                    Id = Guid.NewGuid()
                }
            
            }; // Properly initialized

            _mockMediator.Setup(m => m.Send(It.IsAny<GetUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetUser(Guid.NewGuid(),CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            
            Assert.IsInstanceOf<GetUser.Response>(result.Value);
            Assert.AreEqual(expectedResponse, result.Value);
        }
        
        [Test]
        public async Task CreateUser_ReturnsOkResult()
        {
            // Arrange
            var userDetail = new CreateUser.UserDetail
            {
                // Initialize UserDetail properties here
            };
            _mockMediator.Setup(m => m.Send(It.Is<CreateUser>(cmd => cmd.User == userDetail), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateUser(userDetail, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsInstanceOf<OkResult>(result, "Expected result to be OkResult.");

            // Verify that the mediator was called with the correct parameters
            _mockMediator.Verify(m => m.Send(It.Is<CreateUser>(cmd => cmd.User == userDetail), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test]
        public async Task UpdateUser_ReturnsOkResult()
        {
            // Arrange
            var userDetail = new UpdateUser.UserDetail
            {
                // Initialize UserDetail properties here
            };
            _mockMediator.Setup(m => m.Send(It.Is<UpdateUser>(cmd => cmd.User == userDetail), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateUser(userDetail, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsInstanceOf<OkResult>(result, "Expected result to be OkResult.");

            // Verify that the mediator was called with the correct parameters
            _mockMediator.Verify(m => m.Send(It.Is<UpdateUser>(cmd => cmd.User == userDetail), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}