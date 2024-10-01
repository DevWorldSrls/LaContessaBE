using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IMediator _mediator;

    public DeleteUserHandler(LaContessaDbContext laContessaDbContext, IMediator mediator)
    {
        _laContessaDbContext = laContessaDbContext;
        _mediator = mediator;
    }

    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        var bookings = await _mediator.Send(
            new GetBookingByUserId(userToUpdate.Id.ToString()),
            cancellationToken
        );

        if (bookings != null && bookings.Bookings.Any())
        {
            foreach (var book in bookings.Bookings)
            {
                if (book.Status == Domain.Enums.BookingStatus.Waiting) 
                {
                    await _mediator.Send(
                        new UpdateBooking 
                        {
                            Booking = new UpdateBooking.BookingDetail 
                            {
                                Id = book.Id,
                                UserId = userToUpdate.Id,
                                ActivityId = book.Activity.Id,
                                Date = book.Date,
                                TimeSlot = book.TimeSlot,
                                BookingName = book.BookingName,
                                BookingPrice = book.BookingPrice,
                                IsLesson = book.IsLesson,
                                PaymentPrice = book.PaymentPrice,
                                PhoneNumber = book.PhoneNumber,
                                Status = Domain.Enums.BookingStatus.Cancelled,
                            }
                        },
                        cancellationToken
                    );
                }
            }
        }

        if (userToUpdate.PaymentMethodId != null) 
        {
            await _mediator.Send(
                new DeleteCard { UserId = userToUpdate.Id },
                cancellationToken
            );
        }

        _laContessaDbContext.Users.Remove(userToUpdate);

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}