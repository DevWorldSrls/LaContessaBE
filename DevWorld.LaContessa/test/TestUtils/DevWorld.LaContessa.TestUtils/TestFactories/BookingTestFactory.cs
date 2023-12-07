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
            UserId = Guid.NewGuid().ToString(),
            Date = Guid.NewGuid().ToString(),
            IsLesson = new Random().Next(2) == 0,
            ActivityID = Guid.NewGuid().ToString(),
            Price = new Random().Next(),
            BookingName = Guid.NewGuid().ToString(),
            PhoneNumber = Guid.NewGuid().ToString(),
            TimeSlot = Guid.NewGuid().ToString()
        };

        configure?.Invoke(booking);

        return booking;
    }
}
