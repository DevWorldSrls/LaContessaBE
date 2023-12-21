using DevWorld.LaContessa.Domain.Entities.Bookings;

namespace DevWorld.LaContessa.TestUtils.TestFactories;

public class BookingTestFactory
{
    public static Booking Create(Action<Booking>? configure = null, Guid? idOverride = null)
    {
        var id = idOverride ?? Guid.NewGuid();

        var user = UserTestFactory.Create();
        var activity = ActivityTestFactory.Create();

        var booking = new Booking
        {
            Id = id,
            User = user,
            Date = Guid.NewGuid().ToString(),
            IsLesson = new Random().Next(2) == 0,
            Activity = activity,
            BookingName = Guid.NewGuid().ToString(),
            PhoneNumber = Guid.NewGuid().ToString(),
            TimeSlot = Guid.NewGuid().ToString()
        };

        configure?.Invoke(booking);

        return booking;
    }
}
