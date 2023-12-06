using DevWorld.LaContessa.Domain.Entities.Users;
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
                .Where(x => x.Id == request.Id)
                .Select(x => new GetBooking.Response.BookingDetail
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Date = x.Date,
                    IsLesson = x.IsLesson,
                    activityID = x.activityID,
                    price = x.price,
                    bookingName = x.bookingName,
                    phoneNumber = x.phoneNumber,
                    timeSlot = x.timeSlot
                })
                .FirstOrDefaultAsync(),
        };
    }
}