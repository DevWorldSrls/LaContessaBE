using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace DevWorld.LaContessa.API.Middleware
{
    public class LaContessaProblemDetailsFactory
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IWebHostEnvironment _env;

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
                _ => (int)HttpStatusCode.InternalServerError
            };
            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                httpContext: context,
                statusCode: statusCode,
                title: error.Message,
                type: error.GetType().Name,
                detail: _env.IsDevelopment() ? error.StackTrace : null,
                instance: error.HelpLink
            );
            return problemDetails;
        }
    }
}
