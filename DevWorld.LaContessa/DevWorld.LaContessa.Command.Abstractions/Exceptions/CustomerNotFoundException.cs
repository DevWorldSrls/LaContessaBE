using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class CustomerNotFoundException : CommandException
{
    public CustomerNotFoundException() : base("Customer not found")
    {
    }

    protected CustomerNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected CustomerNotFoundException(string message) : base(message)
    {
    }

    protected CustomerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}