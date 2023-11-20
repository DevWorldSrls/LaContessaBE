using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class SubscriptionAlreadyExistException : CommandException
{
    public SubscriptionAlreadyExistException() : base("Subscription already exist") { }
    protected SubscriptionAlreadyExistException(string message, Exception exception) : base(message, exception) { }
    protected SubscriptionAlreadyExistException(string message) : base(message) { }
    protected SubscriptionAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}