using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class BookingNotFoundException : CommandException
{
    public BookingNotFoundException() : base("Booking not found") { }
    protected BookingNotFoundException(string message, Exception exception) : base(message, exception) { }
    protected BookingNotFoundException(string message) : base(message) { }
    protected BookingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}