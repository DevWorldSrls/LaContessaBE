using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class UserEmailAlreadyExistException : QueryException
{
    public UserEmailAlreadyExistException() : base("User Email already exist")
    {
    }

    protected UserEmailAlreadyExistException(string message, Exception exception) : base(message, exception)
    {
    }

    protected UserEmailAlreadyExistException(string message) : base(message)
    {
    }

    protected UserEmailAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
