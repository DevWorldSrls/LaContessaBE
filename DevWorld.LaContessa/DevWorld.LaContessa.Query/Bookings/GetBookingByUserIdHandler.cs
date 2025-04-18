using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activities;
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
            Bookings = await _laContessaDbContext.Bookings
                .Where(y => !y.IsDeleted)
                .Select(x => new GetBookingByUserId.Response.BookingDetail
                {
                    Id = x.Id,
                    User = new GetBookingByUserId.Response.BookingDetail.UserDetail
                    {
                        Id = x.User.Id,
                        Name = x.User.Name,
                        Surname = x.User.Surname,
                        Email = x.User.Email,
                        ImageProfile = x.User.ImageProfile,
                        IsPro = x.User.IsPro,
                        PeriodicBookingsEnabled = x.User.PeriodicBookingsEnabled,
                        CardNumber = x.User.CardNumber,
                        HasCreditCardLinked = x.User.CustomerId != null,
                        IsDeleted = x.User.IsDeleted,
                    },
                    Date = x.Date,
                    IsLesson = x.IsLesson,
                    Activity = new GetBookingByUserId.Response.BookingDetail.ActivityDetail
                    {
                        Id = x.Activity.Id,
                        Name = x.Activity.Name,
                        IsOutdoor = x.Activity.IsOutdoor,
                        IsSubscriptionRequired = x.Activity.IsSubscriptionRequired,
                        Description = x.Activity.Description,
                        ActivityImg = x.Activity.ActivityImg,
                        Price = x.Activity.Price,
                        Limit = x.Activity.Limit,
                        BookingType = x.Activity.BookingType,
                        Duration = x.Activity.Duration,
                        ExpirationDate = x.Activity.ExpirationDate,
                    },
                    BookingName = x.BookingName,
                    PhoneNumber = x.PhoneNumber,
                    TimeSlot = x.TimeSlot,
                    Status = x.Status,
                    BookingPrice = x.BookingPrice,
                    PaymentPrice = x.PaymentPrice,
                })
                .Where(x => x.User.Id.ToString() == request.UserId)
                .ToArrayAsync(cancellationToken) ?? throw new BookingNotFoundException()
        };
    }
}