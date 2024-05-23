using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;

namespace DevWorld.LaContessa.Domain.Entities.Bookings;

public class Booking : SoftDeletable
{
    public User? User { get; set; }
    public Activity Activity { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string TimeSlot { get; set; } = null!;
    public string BookingName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsLesson { get; set; } = false;
    public BookingStatus Status { get; set; }
    public long BookingPrice { get; set; }
    public long? PaymentPrice { get; set; }
    public string? PaymentIntentId { get; set; }
}
