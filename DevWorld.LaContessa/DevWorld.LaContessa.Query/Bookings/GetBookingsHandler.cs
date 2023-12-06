using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetBookingsHandler : IRequestHandler<GetBookings, GetBookings.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBookingsHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBookings.Response> Handle(GetBookings request, CancellationToken cancellationToken)
    {
        return new GetBookings.Response
        { 
            Bookings = await _laContessaDbContext.Bookings
                .AsQueryable()
                .Select(x => new GetBookings.Response.BookingDetail
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
            }).ToArrayAsync()
        };
    }
}
