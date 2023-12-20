using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class WrongPasswordException : QueryException
{
    public WrongPasswordException() : base("Wrong password entered")
    {
    }

    protected WrongPasswordException(string message, Exception exception) : base(message, exception)
    {
    }

    protected WrongPasswordException(string message) : base(message)
    {
    }

    protected WrongPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}