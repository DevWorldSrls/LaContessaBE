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
        }
    }

}