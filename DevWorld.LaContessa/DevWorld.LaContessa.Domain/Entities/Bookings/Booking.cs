namespace DevWorld.LaContessa.Domain.Entities.Bookings;

public class Booking : SoftDeletable
{
    public string UserId { get; set; } //TODO change to Guid
    public string Date { get; set; } //TODO change to Datetime
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Booking() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
