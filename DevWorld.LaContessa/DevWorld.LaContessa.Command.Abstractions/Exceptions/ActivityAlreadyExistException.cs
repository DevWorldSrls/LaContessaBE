using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class ActivityAlreadyExistException : CommandException
{
    public ActivityAlreadyExistException() : base("User already exist") { }
    protected ActivityAlreadyExistException(string message, Exception exception) : base(message, exception) { }
    protected ActivityAlreadyExistException(string message) : base(message) { }
    protected ActivityAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}