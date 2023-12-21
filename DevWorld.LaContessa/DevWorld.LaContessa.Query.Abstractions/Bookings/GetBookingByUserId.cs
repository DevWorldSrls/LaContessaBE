using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Bookings;

public class GetBookingByUserId : IRequest<GetBookingByUserId.Response>
{
    public GetBookingByUserId(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; } = null!;

    public class Response
    {
        public BookingDetail? Booking { get; set; } = null!;

        public class BookingDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; } = null!;
            public string Date { get; set; } = null!;
            public Activity Activity { get; set; } = null!;
            public string TimeSlot { get; set; } = null!;
            public string BookingName { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public bool IsLesson { get; set; }
        }
    }
}