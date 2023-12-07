using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class BookingAlreadyExistException : CommandException
{
    public BookingAlreadyExistException() : base("Booking already exist")
    {
    }

    protected BookingAlreadyExistException(string message, Exception exception) : base(message, exception)
    {
    }

    protected BookingAlreadyExistException(string message) : base(message)
    {
    }

    protected BookingAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}