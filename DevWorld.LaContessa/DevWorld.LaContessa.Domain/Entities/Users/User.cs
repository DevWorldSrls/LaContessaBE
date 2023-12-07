namespace DevWorld.LaContessa.Domain.Entities.Users;

public class User : SoftDeletable
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CardNumber { get; set; }
    public bool IsPro { get; set; } = false;
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImageProfile { get; set; }
}
