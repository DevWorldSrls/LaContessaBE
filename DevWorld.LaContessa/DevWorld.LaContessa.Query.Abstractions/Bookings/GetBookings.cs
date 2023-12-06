using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetBookings : IRequest<GetBookings.Response>
{
    public class Response
    {
        public BookingDetail[] Bookings { get; set; }

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