namespace DevWorld.LaContessa.Domain.Entities.Users;

public class Booking : SoftDeletable
{
    public string UserId { get; set; }
    public string Date { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Booking() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
