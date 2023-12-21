using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Bookings;

public class GetBookingByUserIdHandler : IRequestHandler<GetBookingByUserId, GetBookingByUserId.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBookingByUserIdHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBookingByUserId.Response> Handle(GetBookingByUserId request,
        CancellationToken cancellationToken)
    {
        return new GetBookingByUserId.Response
        {
            Booking = await _laContessaDbContext.Bookings
                .Select(x => new GetBookingByUserId.Response.BookingDetail
                {
                    Id = x.Id,
                    User = x.User,
                    Date = x.Date,
                    IsLesson = x.IsLesson,
                    Activity = x.Activity,
                    BookingName = x.BookingName,
                    PhoneNumber = x.PhoneNumber,
                    TimeSlot = x.TimeSlot
                })
                .FirstOrDefaultAsync(x => x.User.Id.ToString() == request.UserId, cancellationToken) ?? throw new BookingNotFoundException()
        };
    }
}