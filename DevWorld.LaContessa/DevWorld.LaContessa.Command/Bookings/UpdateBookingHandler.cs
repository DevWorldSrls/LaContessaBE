using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Booking;

public class UpdateBookingHandler : IRequestHandler<UpdateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateBookingHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateBooking request, CancellationToken cancellationToken)
    {
        var bookingToUpdate =
            await _laContessaDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == request.Booking.Id && !x.IsDeleted);

        if (bookingToUpdate is null)
            throw new BookingNotFoundException();

        bookingToUpdate.UserId = request.Booking.UserId;
        bookingToUpdate.Date = request.Booking.Date;
        bookingToUpdate.BookingName = request.Booking.BookingName;
        bookingToUpdate.PhoneNumber = request.Booking.PhoneNumber;
        bookingToUpdate.ActivityID = request.Booking.ActivityID;
        bookingToUpdate.Price = request.Booking.Price;
        bookingToUpdate.IsLesson = request.Booking.IsLesson;
        bookingToUpdate.TimeSlot = request.Booking.TimeSlot;

        await _laContessaDbContext.SaveChangesAsync();
    }
}
