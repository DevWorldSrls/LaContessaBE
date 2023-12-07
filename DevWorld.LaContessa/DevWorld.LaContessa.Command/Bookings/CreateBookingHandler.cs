using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Booking;

public class CreateBookingHandler : IRequestHandler<CreateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateBookingHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateBooking request, CancellationToken cancellationToken)
    {
        var alreadyExist =
            await _laContessaDbContext.Bookings.Where(x => request.Booking.UserId == x.UserId).AnyAsync();

        if (alreadyExist)
            throw new BookingAlreadyExistException();

        var bookingToAdd = new Domain.Entities.Bookings.Booking
        {
            Id = Guid.NewGuid(),
            UserId = request.Booking.UserId,
            Date = request.Booking.Date,
            BookingName = request.Booking.BookingName,
            PhoneNumber = request.Booking.PhoneNumber,
            ActivityID = request.Booking.ActivityId,
            Price = request.Booking.Price,
            IsLesson = request.Booking.IsLesson,
            TimeSlot = request.Booking.TimeSlot
        };

        await _laContessaDbContext.AddAsync(bookingToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
