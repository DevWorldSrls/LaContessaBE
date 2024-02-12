using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class DateValidationException : CommandException
{
    public DateValidationException() : base("Date validation error")
    {
    }

    protected DateValidationException(string message, Exception exception) : base(message, exception)
    {
    }

    protected DateValidationException(string message) : base(message)
    {
    }

    protected DateValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
