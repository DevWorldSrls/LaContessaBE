using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

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
                    Price = x.Price,
                    BookingName = x.BookingName,
                    PhoneNumber = x.PhoneNumber,
                    TimeSlot = x.TimeSlot
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
        };
    }
}