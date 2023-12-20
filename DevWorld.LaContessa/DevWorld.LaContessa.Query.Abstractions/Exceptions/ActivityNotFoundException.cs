using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class ActivityNotFoundException : QueryException
{
    public ActivityNotFoundException() : base("Activity not found")
    {
    }

    protected ActivityNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected ActivityNotFoundException(string message) : base(message)
    {
    }

    protected ActivityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}