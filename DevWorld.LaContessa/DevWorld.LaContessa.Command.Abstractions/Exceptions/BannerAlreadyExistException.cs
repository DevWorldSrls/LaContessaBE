using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Command.Abstractions.Exceptions;

public class BannerAlreadyExistException : CommandException
{
    public BannerAlreadyExistException() : base("Banner already exist")
    {
    }

    protected BannerAlreadyExistException(string message, Exception exception) : base(message, exception)
    {
    }

    protected BannerAlreadyExistException(string message) : base(message)
    {
    }

    protected BannerAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
