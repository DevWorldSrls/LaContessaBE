using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class QueryException : Exception
{
    protected QueryException() : base() { }
    protected QueryException(string message, Exception exception) : base(message, exception) { }
    protected QueryException(string message) : base(message) { }
    protected QueryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
