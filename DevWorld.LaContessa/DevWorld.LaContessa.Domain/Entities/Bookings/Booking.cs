namespace DevWorld.LaContessa.Domain.Entities.Bookings;

public class Booking : SoftDeletable
{
    public string UserId { get; set; }
    public string Date { get; set; }
    public string activityID { get; set; }
    public string timeSlot { get; set; }
    public string bookingName { get; set; }
    public string phoneNumber { get; set; }
    public double price { get; set; }
    public bool IsLesson { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Booking() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
