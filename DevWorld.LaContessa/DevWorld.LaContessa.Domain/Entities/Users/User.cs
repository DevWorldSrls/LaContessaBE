namespace DevWorld.LaContessa.Domain.Entities.Users;

public class User : SoftDeletable
{
    public string Name { get; set; }
    public string Email { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
