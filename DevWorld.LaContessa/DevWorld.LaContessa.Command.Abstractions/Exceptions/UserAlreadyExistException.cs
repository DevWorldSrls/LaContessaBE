using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class UserAlreadyExistException : CommandException
{
    public UserAlreadyExistException() : base("User already exist")
    {
    }

    protected UserAlreadyExistException(string message, Exception exception) : base(message, exception)
    {
    }

    protected UserAlreadyExistException(string message) : base(message)
    {
    }

    protected UserAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
