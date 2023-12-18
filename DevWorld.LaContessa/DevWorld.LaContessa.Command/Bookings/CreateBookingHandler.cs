using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Domain.Entities.Users;
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
            await _laContessaDbContext.Bookings.AnyAsync(x => request.Booking.UserId == x.User.Id.ToString());

        if (alreadyExist)
            throw new BookingAlreadyExistException();
        
        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == new Guid(request.Booking.UserId));
        
        if (user == null)
        {
            // throw an exception or handle any other way you see fit
            throw new UserNotFoundException();
        }

        var bookingToAdd = new Domain.Entities.Bookings.Booking
        {
            Id = Guid.NewGuid(),
            User = user,
            Date = request.Booking.Date,
            BookingName = request.Booking.BookingName,
            PhoneNumber = request.Booking.PhoneNumber,
            Activity = request.Booking.ActivityId,
            Price = request.Booking.Price,
            IsLesson = request.Booking.IsLesson,
            TimeSlot = request.Booking.TimeSlot
        };

        await _laContessaDbContext.AddAsync(bookingToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
