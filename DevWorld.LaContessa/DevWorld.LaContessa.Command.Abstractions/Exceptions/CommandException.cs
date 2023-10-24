using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class CommandException : Exception
{
    protected CommandException() : base() { }
    protected CommandException(string message, Exception exception) : base(message, exception) { }
    protected CommandException(string message) : base(message) { }
    protected CommandException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
