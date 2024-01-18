using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Authentication;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DevWorld.LaContessa.API.Middleware;

[ExcludeFromCodeCoverage]
public class LaContessaProblemDetailsFactory
{
    private readonly IWebHostEnvironment _env;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public LaContessaProblemDetailsFactory(ProblemDetailsFactory problemDetailsFactory, IWebHostEnvironment env)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _env = env;
    }

    public ProblemDetails Create(HttpContext context, Exception error)
    {
        var statusCode = error switch
        {
            QueryException => (int)HttpStatusCode.NotFound,
            CommandException => (int)HttpStatusCode.Conflict,
            AuthenticationException  => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };
        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode,
            error.Message,
            error.GetType().Name,
            _env.IsDevelopment() ? error.StackTrace : null,
            error.HelpLink
        );
        return problemDetails;
    }
}