using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Bookings;

public class UpdateBooking : IRequest
{
    public BookingDetail Booking { get; set; } = null!;

    public class BookingDetail
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string ActivityId { get; set; } = null!;
        public string TimeSlot { get; set; } = null!;
        public string BookingName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public double Price { get; set; }
        public bool IsLesson { get; set; }
    }
}
