using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Bookings;

public class CreateBookingHandler : IRequestHandler<CreateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateBookingHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
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
                TimeSlot = bookingRequest.TimeSlot
            };

            await _laContessaDbContext.AddAsync(bookingToAdd, cancellationToken);
        }
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
