using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Booking;

public class CreateBooking : IRequest
{
    public BookingDetail Booking { get; set; } = null!;
       
    public class BookingDetail
    {
        public string UserId { get; set; }
        public string Date { get; set; }
    }
}
