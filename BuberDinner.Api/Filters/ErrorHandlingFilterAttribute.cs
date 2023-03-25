using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        //If an exception is thrown and is not handled, this method is invoked
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDatails = new ProblemDetails //RFC 7807 standard for apis
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1", //standardized url for details of the problem and status code
                Title = "An error occurred while processing your request",
                Status = (int)HttpStatusCode.InternalServerError,

            };

            context.Result = new ObjectResult(problemDatails);

            context.ExceptionHandled = true;
        }
    }
}