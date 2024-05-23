using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Bookings;

public class CreateBooking : IRequest
{
    public BookingDetail[] Bookings { get; set; } = null!;

    public class BookingDetail
    {
        public Guid? UserId { get; set; }
        public Guid ActivityId { get; set; }
        public string Date { get; set; } = null!;
        public string TimeSlot { get; set; } = null!;
        public string BookingName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsLesson { get; set; } = false;
        public BookingStatus Status { get; set; }
        public long BookingPrice { get; set; }
        public long? PaymentPrice { get; set; }
    }
}
