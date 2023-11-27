using DevWorld.LaContessa.Domain.Entities.Bookings;

namespace DevWorld.LaContessa.TestUtils.TestFactories;

public class BookingTestFactory
{
    public static Booking Create(Action<Booking>? configure = null, Guid? idOverride = null)
    {
        var id = idOverride ?? Guid.NewGuid();

        var booking = new Booking
        {
            Id = id,
            Date = DateTime.UtcNow.ToString(),
            UserId = Guid.NewGuid().ToString(),
            IsDeleted = false,
            // ...
        };

        configure?.Invoke(booking);

        return booking;
    }
}
