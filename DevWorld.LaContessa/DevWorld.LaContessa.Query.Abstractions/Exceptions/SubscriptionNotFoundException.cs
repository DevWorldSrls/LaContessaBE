using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class SubscriptionNotFoundException : QueryException
{
    public SubscriptionNotFoundException() : base("Subscription not found")
    {
    }

    protected SubscriptionNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected SubscriptionNotFoundException(string message) : base(message)
    {
    }

    protected SubscriptionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}