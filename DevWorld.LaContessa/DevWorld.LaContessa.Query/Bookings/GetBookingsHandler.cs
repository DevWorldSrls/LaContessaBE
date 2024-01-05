using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Bookings;

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
                }).ToArrayAsync()
        };
    }
}
