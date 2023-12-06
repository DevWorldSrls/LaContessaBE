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
            activityID = Guid.NewGuid().ToString(),
            price = new Random().Next(),
            bookingName = Guid.NewGuid().ToString(),
            phoneNumber = Guid.NewGuid().ToString(),
            timeSlot = Guid.NewGuid().ToString()
        };

        configure?.Invoke(booking);

        return booking;
    }
}
