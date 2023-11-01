using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class UserNotFoundException : CommandException
{
    public UserNotFoundException() : base("User not found") { }
    protected UserNotFoundException(string message, Exception exception) : base(message, exception) { }
    protected UserNotFoundException(string message) : base(message) { }
    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}