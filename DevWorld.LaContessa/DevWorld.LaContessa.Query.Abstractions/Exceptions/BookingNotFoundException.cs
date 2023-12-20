using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class BookingNotFoundException : QueryException
{
    public BookingNotFoundException() : base("Booking not found")
    {
    }

    protected BookingNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected BookingNotFoundException(string message) : base(message)
    {
    }

    protected BookingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}