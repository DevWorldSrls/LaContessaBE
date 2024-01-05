using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Bookings;

public class CreateBookingHandler : IRequestHandler<CreateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IMediator _mediator;

    public CreateBookingHandler(
        LaContessaDbContext laContessaDbContext,
        IMediator mediator
        )
    {
        _laContessaDbContext = laContessaDbContext;
        _mediator = mediator;
    }

    public async Task Handle(CreateBooking request, CancellationToken cancellationToken)
    {
        foreach (var bookingRequest in request.Bookings)
        {
            var alreadyExist = await _laContessaDbContext.Bookings.AnyAsync(x => 
                bookingRequest.UserId == x.User.Id.ToString() &&
                bookingRequest.ActivityId  == x.Activity.Id.ToString() &&
                bookingRequest.Date  == x.Date &&
                bookingRequest.TimeSlot  == x.TimeSlot,
            cancellationToken);

            if (alreadyExist)
                throw new BookingAlreadyExistException();

            var user = await _laContessaDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(bookingRequest.UserId), cancellationToken) ?? throw new UserNotFoundException();

            var activity = await _laContessaDbContext.Activities
                .FirstOrDefaultAsync(a => a.Id == Guid.Parse(bookingRequest.ActivityId), cancellationToken) ?? throw new ActivityNotFoundException();

            switch (activity.BookingType)
            {
                case Domain.Enums.ActivityBookingType.APM:

                    var bookingForActivity = await _laContessaDbContext.Bookings.CountAsync(x => x.Activity.Id == activity.Id && x.Date == bookingRequest.Date && x.TimeSlot == bookingRequest.TimeSlot);

                    if (bookingForActivity >= activity.Limit)
                    {
                        var activityDateAPM = activity.DateList.FirstOrDefault(x => x.Date == bookingRequest.Date) ?? throw new ActivityNotFoundException();
                        var timeSlotToUpdateAPM = activityDateAPM.TimeSlotList.FirstOrDefault(x => x.TimeSlot == bookingRequest.TimeSlot) ?? throw new ActivityNotFoundException();
                        timeSlotToUpdateAPM.IsAlreadyBooked = true;
                    }

                    break;
                case Domain.Enums.ActivityBookingType.APS:

                    var activityDateAPS = activity.DateList.FirstOrDefault(x => x.Date == bookingRequest.Date) ?? throw new ActivityNotFoundException();
                    var timeSlotToUpdateAPS = activityDateAPS.TimeSlotList.FirstOrDefault(x => x.TimeSlot == bookingRequest.TimeSlot) ?? throw new ActivityNotFoundException();
                    timeSlotToUpdateAPS.IsAlreadyBooked = true;

                    break;
                default:
                    break;
            }

            var bookingToAdd = new Domain.Entities.Bookings.Booking
            {
                Id = Guid.NewGuid(),
                User = user,
                Date = bookingRequest.Date,
                BookingName = bookingRequest.BookingName,
                PhoneNumber = bookingRequest.PhoneNumber,
                Activity = activity,
                IsLesson = bookingRequest.IsLesson,
                TimeSlot = bookingRequest.TimeSlot,
                Status = bookingRequest.Status,
                BookingPrice = bookingRequest.BookingPrice,
                PaymentPrice = bookingRequest.PaymentPrice,
            };

            await _laContessaDbContext.AddAsync(bookingToAdd, cancellationToken);

            if(bookingToAdd.Status == Domain.Enums.BookingStatus.Payed && bookingToAdd.PaymentPrice != null)
            {
                await _mediator.Send(
                    new CreateStripePaymentRequest
                    {
                        CustomerId = user.CustomerId ?? throw new Exception(), //TODO: Use specific exception
                        Amount = bookingToAdd.PaymentPrice ?? 0,
                        Currency = "EUR",
                        Description = "Pagamento " + activity.Name,
                        ReceiptEmail = "info@lacontessa.it",
                    },
                    cancellationToken
                );
            }
        }
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
