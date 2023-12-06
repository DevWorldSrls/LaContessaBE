using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetBookingByUserId : IRequest<GetBookingByUserId.Response>
{
    public string UserId { get; set; }

    public GetBookingByUserId(string userId)
    {
        UserId = userId;
    }

    public class Response
    {
        public BookingDetail? Booking { get; set; }

        public class BookingDetail
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public string Date { get; set; }
            public string activityID { get; set; }
            public string timeSlot { get; set; }
            public string bookingName { get; set; }
            public string phoneNumber { get; set; }
            public double price { get; set; }
            public bool IsLesson { get; set; }
        }
    }

}