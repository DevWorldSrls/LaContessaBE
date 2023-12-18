using DevWorld.LaContessa.Domain.Entities.Users;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetBooking : IRequest<GetBooking.Response>
{
    public GetBooking(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    public class Response
    {
        public BookingDetail? Booking { get; set; }

        public class BookingDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; }
            public string Date { get; set; }
            public string ActivityID { get; set; }
            public string TimeSlot { get; set; }
            public string BookingName { get; set; }
            public string PhoneNumber { get; set; }
            public double Price { get; set; }
            public bool IsLesson { get; set; }
        }
    }
}
