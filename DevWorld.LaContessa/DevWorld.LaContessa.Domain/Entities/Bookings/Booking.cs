using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;

namespace DevWorld.LaContessa.Domain.Entities.Bookings;

public class Booking : SoftDeletable
{
    public User User { get; set; } = null!; 
    public string Date { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public string TimeSlot { get; set; } = null!;
    public string BookingName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsLesson { get; set; }
    public BookingStatus Status { get; set; }
}
