using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetBooking : IRequest<GetBooking.Response>
{
    public Guid Id { get; set; }

    public GetBooking(Guid id)
    {
        Id = id;
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
