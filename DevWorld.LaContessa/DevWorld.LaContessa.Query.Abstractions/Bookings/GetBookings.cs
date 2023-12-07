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
            public string ActivityId { get; set; }
            public string TimeSlot { get; set; }
            public string BookingName { get; set; }
            public string PhoneNumber { get; set; }
            public double Price { get; set; }
            public bool IsLesson { get; set; }
        }
    }
}