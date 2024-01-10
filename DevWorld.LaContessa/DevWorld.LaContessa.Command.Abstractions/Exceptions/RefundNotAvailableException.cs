using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class RefundNotAvailableException : CommandException
{
    public RefundNotAvailableException() : base("Refund not available")
    {
    }

    protected RefundNotAvailableException(string message, Exception exception) : base(message, exception)
    {
    }

    protected RefundNotAvailableException(string message) : base(message)
    {
    }

    protected RefundNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
