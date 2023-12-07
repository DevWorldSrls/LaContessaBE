namespace DevWorld.LaContessa.Domain.Entities.Bookings;

public class Booking : SoftDeletable
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string UserId { get; set; }
    public string Date { get; set; }
    public string ActivityID { get; set; }
    public string TimeSlot { get; set; }
    public string BookingName { get; set; }
    public string PhoneNumber { get; set; }
    public double Price { get; set; }
    public bool IsLesson { get; set; }
}
