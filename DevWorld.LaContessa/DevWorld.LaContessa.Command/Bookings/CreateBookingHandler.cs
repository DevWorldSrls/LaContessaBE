using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Bookings;

public class CreateBookingHandler : IRequestHandler<CreateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateBookingHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateBooking request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Bookings.AnyAsync(x => request.Booking.UserId == x.User.Id.ToString(), cancellationToken);

        if (alreadyExist)
            throw new BookingAlreadyExistException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(request.Booking.UserId), cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == Guid.Parse(request.Booking.ActivityId), cancellationToken) ?? throw new ActivityNotFoundException();

        var bookingToAdd = new Domain.Entities.Bookings.Booking
        {
            Id = Guid.NewGuid(),
            User = user,
            Date = request.Booking.Date,
            BookingName = request.Booking.BookingName,
            PhoneNumber = request.Booking.PhoneNumber,
            Activity = activity,
            Price = request.Booking.Price,
            IsLesson = request.Booking.IsLesson,
            TimeSlot = request.Booking.TimeSlot
        };

        await _laContessaDbContext.AddAsync(bookingToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
