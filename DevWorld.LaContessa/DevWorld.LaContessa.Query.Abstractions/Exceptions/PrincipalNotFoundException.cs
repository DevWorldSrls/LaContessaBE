using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class PrincipalNotFoundException : QueryException
{
    public PrincipalNotFoundException() : base("Principal not found")
    {
    }

    protected PrincipalNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected PrincipalNotFoundException(string message) : base(message)
    {
    }

    protected PrincipalNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}