using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Bookings;

public class GetBookingHandler : IRequestHandler<GetBooking, GetBooking.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBookingHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBooking.Response> Handle(GetBooking request, CancellationToken cancellationToken)
    {
        return new GetBooking.Response
        {
            Booking = await _laContessaDbContext.Bookings
                .Select(x => new GetBooking.Response.BookingDetail
                {
                    Id = x.Id,
                    User = x.User,
                    Date = x.Date,
                    IsLesson = x.IsLesson,
                    Activity = x.Activity,
                    BookingName = x.BookingName,
                    PhoneNumber = x.PhoneNumber,
                    TimeSlot = x.TimeSlot,
                    Status = x.Status,
                    BookingPrice = x.BookingPrice,
                    PaymentPrice = x.PaymentPrice,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new BookingNotFoundException()
        };
    }
}