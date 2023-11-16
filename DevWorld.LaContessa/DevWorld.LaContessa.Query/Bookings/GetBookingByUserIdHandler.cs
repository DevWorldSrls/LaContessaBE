using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetBookingByUserIdHandler : IRequestHandler<GetBookingByUserId, GetBookingByUserId.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBookingByUserIdHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBookingByUserId.Response> Handle(GetBookingByUserId request, CancellationToken cancellationToken)
    {
        return new GetBookingByUserId.Response
        {
            Booking = await _laContessaDbContext.Bookings
                .Where(x => x.UserId == request.UserId)
                .Select(x => new GetBookingByUserId.Response.BookingDetail
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Date = x.Date
                })
                .FirstOrDefaultAsync(),
        };
    }
}