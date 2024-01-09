namespace DevWorld.LaContessa.Domain.Entities.Users;

public class User : SoftDeletable
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsPro { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public bool PeriodicBookingsEnabled { get; set; } = false;
    public string? ImageProfile { get; set; }
    public string? RefreshToken { get; set; }
    public string? CustomerId { get; set; }
    public string? PaymentMethodId { get; set; }
}
