using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class PaymentIntentNotFoundException : CommandException
{
    public PaymentIntentNotFoundException() : base("PaymentIntent not found")
    {
    }

    protected PaymentIntentNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected PaymentIntentNotFoundException(string message) : base(message)
    {
    }

    protected PaymentIntentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}