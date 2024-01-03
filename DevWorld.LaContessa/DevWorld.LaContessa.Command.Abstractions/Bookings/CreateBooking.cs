using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Bookings;

public class CreateBooking : IRequest
{
    public BookingDetail[] Bookings { get; set; } = null!;

    public class BookingDetail
    {
        public string UserId { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string ActivityId { get; set; } = null!;
        public string TimeSlot { get; set; } = null!;
        public string BookingName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsLesson { get; set; }
    }
}
