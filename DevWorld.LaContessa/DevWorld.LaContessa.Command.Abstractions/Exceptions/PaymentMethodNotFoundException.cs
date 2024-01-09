using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class PaymentMethodNotFoundException : CommandException
{
    public PaymentMethodNotFoundException() : base("PaymentMethod not found")
    {
    }

    protected PaymentMethodNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected PaymentMethodNotFoundException(string message) : base(message)
    {
    }

    protected PaymentMethodNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}