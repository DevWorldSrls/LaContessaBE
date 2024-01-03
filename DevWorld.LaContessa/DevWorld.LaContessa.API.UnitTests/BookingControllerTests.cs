//using DevWorld.LaContessa.API.Controllers;
//using DevWorld.LaContessa.Command.Abstractions.Bookings;
//using DevWorld.LaContessa.Query.Abstractions.Bookings;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace DevWorld.LaContessa.API.UnitTests;

//[TestFixture]
//public class BookingControllerTests
//{
//    [SetUp]
//    public void Setup()
//    {
//        _mockMediator = new Mock<IMediator>();
//        _controller = new BookingController(_mockMediator.Object);
//    }

//    private Mock<IMediator> _mockMediator;
//    private BookingController _controller;

//    [Test]
//    public async Task GetBookings_ReturnsExpectedResult()
//    {
//        // Arrange
//        var expectedResponse = new GetBookings.Response
//        {
//            Bookings = new[]
//            {
//                new GetBookings.Response.BookingDetail { Id = Guid.NewGuid() },
//                new GetBookings.Response.BookingDetail { Id = Guid.NewGuid() }
//            }
//        };
//        _mockMediator.Setup(m => m.Send(It.IsAny<GetBookings>(), It.IsAny<CancellationToken>()))
//            .ReturnsAsync(expectedResponse);

//        // Act
//        var result = await _controller.GetBookings(CancellationToken.None);

//        // Assert
//        Assert.IsNotNull(result);
//        Assert.IsInstanceOf<ActionResult<GetBookings.Response>>(result);
//        Assert.That(result.Value, Is.EqualTo(expectedResponse));
//    }

//    [Test]
//    public async Task GetBooking_ReturnsExpectedResult()
//    {
//        // Arrange
//        var expectedId = Guid.NewGuid();
//        var expectedResponse = new GetBooking.Response
//        {
//            Booking = new GetBooking.Response.BookingDetail { Id = expectedId }
//        };
//        _mockMediator.Setup(m => m.Send(It.Is<GetBooking>(cmd => cmd.Id == expectedId), It.IsAny<CancellationToken>()))
//            .ReturnsAsync(expectedResponse);

//        // Act
//        var result = await _controller.GetBooking(expectedId, CancellationToken.None);

//        // Assert
//        Assert.IsNotNull(result);
//        Assert.IsInstanceOf<ActionResult<GetBooking.Response>>(result);
//        Assert.That(result.Value, Is.EqualTo(expectedResponse));
//    }

//    [Test]
//    public async Task CreateBooking_ReturnsOkResult()
//    {
//        // Arrange
//        var bookingDetail = new CreateBooking.BookingDetail();
//        _mockMediator.Setup(m =>
//                m.Send(It.Is<CreateBooking>(cmd => cmd.Bookings == bookingDetail), It.IsAny<CancellationToken>()))
//            .Returns(Task.CompletedTask);

//        // Act
//        var result = await _controller.CreateBooking(bookingDetail, CancellationToken.None);

//        // Assert
//        Assert.IsNotNull(result);
//        Assert.IsInstanceOf<OkResult>(result);
//    }

//    [Test]
//    public async Task UpdateBooking_ReturnsOkResult()
//    {
//        // Arrange
//        var bookingDetail = new UpdateBooking.BookingDetail();
//        _mockMediator.Setup(m =>
//                m.Send(It.Is<UpdateBooking>(cmd => cmd.Booking == bookingDetail), It.IsAny<CancellationToken>()))
//            .Returns(Task.CompletedTask);

//        // Act
//        var result = await _controller.UpdateBooking(bookingDetail, CancellationToken.None);

//        // Assert
//        Assert.IsNotNull(result);
//        Assert.IsInstanceOf<OkResult>(result);
//    }

//    [Test]
//    public async Task GetBookingByUserId_ReturnsExpectedResult()
//    {
//        // Arrange
//        var userId = "someUserId";
//        var expectedResponse = new GetBookingByUserId.Response();
//        _mockMediator.Setup(m =>
//                m.Send(It.Is<GetBookingByUserId>(cmd => cmd.UserId == userId), It.IsAny<CancellationToken>()))
//            .ReturnsAsync(expectedResponse);

//        // Act
//        var result = await _controller.GetBooking(userId, CancellationToken.None);

//        // Assert
//        Assert.IsNotNull(result);
//        Assert.IsInstanceOf<ActionResult<GetBookingByUserId.Response>>(result);
//        var actionResult = result.Value;
//        Assert.That(actionResult, Is.EqualTo(expectedResponse));
//    }
//}