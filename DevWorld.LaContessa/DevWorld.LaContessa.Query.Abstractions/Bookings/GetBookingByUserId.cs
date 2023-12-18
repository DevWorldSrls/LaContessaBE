using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetBookingByUserId : IRequest<GetBookingByUserId.Response>
{
    public GetBookingByUserId(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }

    public class Response
    {
        public BookingDetail? Booking { get; set; }

        public class BookingDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; }
            public string Date { get; set; }
            public Activity Activity { get; set; }
            public string TimeSlot { get; set; }
            public string BookingName { get; set; }
            public string PhoneNumber { get; set; }
            public double Price { get; set; }
            public bool IsLesson { get; set; }
        }
    }
}