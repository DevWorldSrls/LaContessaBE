namespace DevWorld.LaContessa.Domain.Entities.Users;

public class User : SoftDeletable
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? CardNumber { get; set; }
    public bool IsPro { get; set; } = false;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? ImageProfile { get; set; }
    public string? RefreshToken { get; set; }
}
