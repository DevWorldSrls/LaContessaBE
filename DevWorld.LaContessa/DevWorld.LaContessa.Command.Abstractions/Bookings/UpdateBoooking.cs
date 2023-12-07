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
        public string ActivityID { get; set; }
        public string TimeSlot { get; set; }
        public string BookingName { get; set; }
        public string PhoneNumber { get; set; }
        public double Price { get; set; }
        public bool IsLesson { get; set; }
    }
}
