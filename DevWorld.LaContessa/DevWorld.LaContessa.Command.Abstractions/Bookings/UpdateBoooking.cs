using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Booking;

public class UpdateBooking : IRequest
{
    public BookingDetail Booking { get; set; } = null!;
       
    public class BookingDetail
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
    }
}
