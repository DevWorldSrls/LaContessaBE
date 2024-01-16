﻿using System.Runtime.Serialization;

namespace DevWorld.LaContessa.Query.Abstractions.Exceptions;

public class BannerNotFoundException : QueryException
{
    public BannerNotFoundException() : base("Banner not found")
    {
    }

    protected BannerNotFoundException(string message, Exception exception) : base(message, exception)
    {
    }

    protected BannerNotFoundException(string message) : base(message)
    {
    }

    protected BannerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
